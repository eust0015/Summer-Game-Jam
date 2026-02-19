using System;
using System.Collections.Generic;
using UnityEngine;

public class ToiletShark : MonoBehaviour, IInteractable
{
	public float InteractionTime { get; set; } = 0f;
	Animator anim;

	[SerializeField] private GameObject explosion;
	[SerializeField] private GameObject playerSharkRepellant;



	private bool ready = false, hasBeenUsed = false;

	public void setReady(bool value)
	{
		ready = value;
		Debug.Log("Shark is now ready: " + ready);
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
		Debug.Log("Shark focused");
	}

	public void OnUnfocus()
	{
		anim?.SetBool("isFocused", false);
		Debug.Log("Shark Unfocused");

	}

	public void OnInteract(GameObject obj)
	{
		if (ready && !hasBeenUsed)
		{
			Debug.Log("Interacted with shark");
			hasBeenUsed = true;
			if (playerSharkRepellant != null)
			{
				StartCoroutine(ActivateRepellantAndExplode());
			}
			else
			{
				ExplodeAndDestroy();
			}
		}
	}

	private System.Collections.IEnumerator ActivateRepellantAndExplode()
	{
		playerSharkRepellant.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine(ShrinkAndMoveDown());
		ExplodeAndDestroy();
	}

	private System.Collections.IEnumerator ShrinkAndMoveDown()
	{
		float duration = 0.25f;
		float elapsed = 0f;
		Vector3 startScale = transform.localScale;
		Vector3 endScale = startScale * 0.1f;
		Vector3 startPos = transform.position;
		Vector3 endPos = startPos + Vector3.down * 0.2f;
		while (elapsed < duration)
		{
			float t = elapsed / duration;
			transform.localScale = Vector3.Lerp(startScale, endScale, t);
			transform.position = Vector3.Lerp(startPos, endPos, t);
			elapsed += Time.deltaTime;
			yield return null;
		}
		transform.localScale = endScale;
		transform.position = endPos;
	}

	private void ExplodeAndDestroy()
	{
		if (explosion != null)
		{
			// Instantiate(explosion, transform.position, Quaternion.identity);
		}
		Destroy(this.gameObject);
		if (playerSharkRepellant != null)
			playerSharkRepellant.SetActive(false);
	}

	public void Drop()
	{
		// Implement drop logic here
	}

	private void Awake()
	{
		// anim = TryGetComponent<Animator>(out Animator animator) ? animator : null;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

}