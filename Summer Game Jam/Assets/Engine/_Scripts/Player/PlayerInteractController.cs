using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractDuration { float InteractionTime { get; } }
public interface IInteractValidator
{
	bool CanInteract(GameObject interactor, out string reason);
}

public class PlayerInteractController : MonoBehaviour
{
	PlayerInput playerInput;

	[Header("References")]
	[SerializeField] private PlayerTargetSystem targetSystem;

	[Header("Interaction Config")]
	[SerializeField] private float defaultInteractDuration = 0.5f;
	[SerializeField] private float maxInteractionDistance = 3f;
	[SerializeField] private bool cancelIfTargetChanges = true;

	[Header("Cooldown")]
	[SerializeField] private float interactionCooldown = 0.15f;

	private IInteractable current;
	private float timer;
	private bool isInteracting;
	private float lastInteractionTime;
	private float currentDuration;

	public bool IsInteracting => isInteracting;
	public float Progress => currentDuration <= 0f ? 1f : Mathf.Clamp01(timer / currentDuration);
	public IInteractable CurrentTarget => current;

	private void OnInteractInput(InputAction.CallbackContext ctx)
	{
		if (ctx.started)
		{

			TryBeginInteract();
		}
		else if (ctx.canceled)
		{
			CancelInteract("[cancel] released");
		}
	}

	private void TryBeginInteract()
	{
		if (Time.time < lastInteractionTime + interactionCooldown) return;

		IInteractable target = targetSystem.CurrentTarget as IInteractable;

		if (target == null) return;

		if (target is IInteractValidator validator && !validator.CanInteract(gameObject, out string reason))
		{
			Debug.Log("[Interact] Cannot interact: " + reason);
			return;
		}

		if (!WithinDistance(target))
		{
			Debug.Log("[Interact] Target out of range");
			return;
		}

		float dur = defaultInteractDuration;

		if (target is IInteractDuration durTarget)
		{
			dur = durTarget.InteractionTime;
		}
		currentDuration = Mathf.Max(0f, dur);

		current = target;
		timer = 0f;
		isInteracting = true;

		if (currentDuration <= 0f)
			FinishInteract();
	}


	private void FinishInteract()
	{
		if (!isInteracting) return;

		if (!WithinDistance(current))
		{
			CancelInteract("[finish] target out of range");
			return;
		}

		current.OnInteract(gameObject);
		current.InteractProgress(0f);
		lastInteractionTime = Time.time;

		ResetState();
	}

	private void CancelInteract(string reason)
	{
		ResetState();
	}

	private void ResetState()
	{
		isInteracting = false;
		current = null;
		timer = 0f;
		currentDuration = 0f;

		if (targetSystem.CurrentTarget is IInteractable)
			targetSystem.CurrentTarget.InteractProgress(0f);
	}

	private bool WithinDistance(IInteractable target)
	{
		Vector3 p = target.GetTargetPoint();
		float dst = Vector3.Distance(transform.position, p);
		return dst <= maxInteractionDistance + 0.001f;
	}


	private void Awake()
	{
		if (!targetSystem) targetSystem = GetComponent<PlayerTargetSystem>();
		playerInput = GetComponent<PlayerInput>();
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (!IsInteracting) return;

		if (cancelIfTargetChanges && !ReferenceEquals(targetSystem.CurrentTarget, current))
		{
			CancelInteract("[update] target changed");
			return;
		}

		if (currentDuration > 0f)
		{
			timer += Time.deltaTime;

			targetSystem.CurrentTarget.InteractProgress(timer / currentDuration);

			if (timer >= currentDuration)
			{
				FinishInteract();
			}
		}
	}

	private void OnEnable()
	{
		playerInput.actions["Interact"].performed += ctx => OnInteractInput(ctx);
		playerInput.actions["Interact"].canceled += ctx => OnInteractInput(ctx);
	}

	private void OnDisable()
	{
		playerInput.actions["Interact"].performed -= ctx => OnInteractInput(ctx);
		playerInput.actions["Interact"].canceled -= ctx => OnInteractInput(ctx);
	}
}
