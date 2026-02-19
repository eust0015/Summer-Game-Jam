using System;
using System.Collections.Generic;
using UnityEngine;

public class SharkRepellant : MonoBehaviour, IInteractable
{
	public float InteractionTime { get; set; } = 0f;
	public ToiletShark shark;
	Animator anim;
	private bool ready = false, hasBeenUsed = false;

	public void setReady(bool value)
	{
		ready = value;
		Debug.Log("Shark Repellant is now ready: " + ready);
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
		Debug.Log("Shark Repellant focused");
	}

	public void OnUnfocus()
	{
		anim?.SetBool("isFocused", false);
		Debug.Log("Shark Repellant Unfocused");
		// Code to execute when the door loses focus
	}

	public void OnInteract(GameObject obj)
	{
		if (ready && !hasBeenUsed)
		{
			Debug.Log("Interacted with shark repellant");
			hasBeenUsed = true;
			if (shark != null)
			{
				shark.setReady(true);
			}
			Destroy(this.gameObject);
		}
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
