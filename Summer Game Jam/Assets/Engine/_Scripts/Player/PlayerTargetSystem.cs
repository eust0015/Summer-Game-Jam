using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerTargetSystem : MonoBehaviour
{
	public static PlayerTargetSystem Instance;

	[Header("Ray Properties")]
	[SerializeField] LayerMask targetLayerMask;
	[SerializeField] float distance;
	[SerializeField] LayerMask occlusionLayerMask;

	Vector2 screenCenter = Vector2.zero;

	private IInteractable currentTarget;

	public IInteractable CurrentTarget => currentTarget;

	void FocusStart(IInteractable interactable)
	{
		if (interactable == null || interactable == currentTarget) return;

		currentTarget = interactable;

		if (currentTarget != null)
		{
			currentTarget.OnFocus();
		}
	}

	void FocusEnd()
	{
		if (currentTarget != null)
		{
			currentTarget.OnUnfocus();
			currentTarget = null;
		}
	}

	IInteractable GetInteractable(GameObject target)
	{
		IInteractable interactable = target.GetComponent<IInteractable>() ?? target.GetComponentInParent<IInteractable>();

		if (interactable == null)
		{
			return null;
		}

		return interactable;
	}

	// Checks if there is a clear line of sight between the camera transform position and the target
	bool HasClearLOS(RaycastHit hit)
	{
		Vector3 dir = Camera.main.transform.position - hit.point;

		float max = Mathf.Max(0f, hit.distance - 0.01f);

		if (Physics.Raycast(hit.point, dir.normalized, out RaycastHit losHit, max, occlusionLayerMask, QueryTriggerInteraction.Ignore))
		{
			Debug.DrawLine(hit.point, losHit.point, Color.red, 1f);
			return false;
		}

		return true;
	}

	void HandleRaycast()
	{
		Ray ray = Camera.main.ScreenPointToRay(screenCenter);

		Debug.DrawRay(ray.origin, ray.direction * distance, Color.white);

		if (Physics.Raycast(ray, out RaycastHit hitInfo, distance, targetLayerMask))
		{
			if (!HasClearLOS(hitInfo))
			{
				Debug.Log("LOS Blocked");
				FocusEnd();
				return;
			}
			else
			{
				FocusStart(GetInteractable(hitInfo.collider.gameObject));

				Debug.DrawLine(ray.origin, hitInfo.point, Color.green);
			}
		}
		else
		{
			FocusEnd();
			return;
		}
	}

	private void Awake()
	{
		Instance = this;
		screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	private void FixedUpdate()
	{
		HandleRaycast();
	}

	void OnGUI()
	{
		if (Application.isEditor)  // or check the app debug flag
		{
			GUI.Label(new Rect(0, 0, 100, 20), $"");
		}
	}
}
