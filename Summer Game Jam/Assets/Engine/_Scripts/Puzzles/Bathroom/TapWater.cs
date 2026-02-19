using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TapWater : MonoBehaviour, IInteractable
{
	public float InteractionTime { get; set; } = 0f;
	public GameObject sinkWater, thirstTimerCanvas;
	public Plunger plunger;
	Animator anim;
	private bool ready = false, hasBeenUsed = false;
	[SerializeField] private GameObject glassOfWater;
	[SerializeField] private Transform sinkCupTransform, parentCam;
	[SerializeField] private Transform drinkingTransform;

	[Header("Glass Movement")]
	[SerializeField] private float glassMoveDuration = 1.5f;

	public void setReady(bool value)
	{
		ready = value;
		Debug.Log("Tap water is now ready: " + ready);
	}

	public Vector3 GetTargetPoint()
	{
		return transform.position;
	}
	// public override void Use()
    // {
    //     Debug.Log("Used item");
    // }

	public void OnFocus()
	{
		anim?.SetBool("isFocused", true);
		Debug.Log("Tap water focused");
	}

	public void OnUnfocus()
	{
		anim?.SetBool("isFocused", false);
		Debug.Log("Tap water Unfocused");
	}

	public void OnInteract(GameObject obj)
	{
		if (ready && !hasBeenUsed)
		{
		Debug.Log("Interacted with tap water");

		if (glassOfWater != null && sinkCupTransform != null)
			{
				Debug.Log("Instantiating glass of water at sink cup position");
				GameObject spawned = null;
				if (parentCam != null)
				{
					spawned = Instantiate(glassOfWater, sinkCupTransform.position, sinkCupTransform.rotation, parentCam);
				}
				else
				{
					spawned = Instantiate(glassOfWater, sinkCupTransform.position, sinkCupTransform.rotation);
				}
				if (spawned != null)
				{
					StartCoroutine(MoveGlassToTarget(spawned.transform));
				}
			}
			plunger.setReady(true);
			sinkWater.SetActive(true);

			hasBeenUsed = true;
			StartCoroutine(DisableThirstTimerAfterDelay());
		}
	}

	private IEnumerator MoveGlassToTarget(Transform t)
	{
		float elapsed = 0f;
		Vector3 startPos = t.localPosition;
		Quaternion startRot = t.localRotation;
		Vector3 startScale = t.localScale;

		if (drinkingTransform == null || parentCam == null)
		{
			Debug.LogWarning("drinkingTransform or parentCam not assigned!");
			yield break;
		}

		Vector3 endLocalPos = drinkingTransform.localPosition;
		Quaternion endLocalRot = drinkingTransform.localRotation;
		Vector3 endLocalScale = drinkingTransform.localScale;

		while (elapsed < glassMoveDuration)
		{
			float k = Mathf.Clamp01(elapsed / glassMoveDuration);
			t.localPosition = Vector3.Lerp(startPos, endLocalPos, k);
			t.localRotation = Quaternion.Slerp(startRot, endLocalRot, k);
			t.localScale = Vector3.Lerp(startScale, endLocalScale, k);
			elapsed += Time.deltaTime;
			yield return null;
		}

		// Ensure final placement
		t.localPosition = endLocalPos;
		t.localRotation = endLocalRot;
		t.localScale = endLocalScale;

		yield return new WaitForSeconds(1f);
		if (t != null)
		{
			Destroy(t.gameObject);
		}
	}

	private IEnumerator DisableThirstTimerAfterDelay()
	{
		yield return new WaitForSeconds(1f);
		if (thirstTimerCanvas != null)
		{
			thirstTimerCanvas.SetActive(false);
		}
	}

	public void Drop()
	{
		// Implement drop logic here
	}

	private void Awake()
	{
		anim = TryGetComponent<Animator>(out Animator animator) ? animator : null;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}
}
