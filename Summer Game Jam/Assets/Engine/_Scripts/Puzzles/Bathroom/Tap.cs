using System;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour, IInteractable
{
	public float InteractionTime { get; set; } = 0f;
	public GameObject tapWater, sinkWater;
	Animator anim;
	private bool ready = true, hasBeenUsed = false;

	public void setReady(bool value)
	{
		ready = value;
		Debug.Log("Tap is now ready: " + ready);
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
		Debug.Log("Tap focused");
	}

	public void OnUnfocus()
	{
		anim?.SetBool("isFocused", false);
		Debug.Log("Tap Unfocused");
	}

	public void OnInteract(GameObject obj)
	{
		if (ready && !hasBeenUsed)
		{
		Debug.Log("Interacted with tap");
		// Use();
        // Destroy(this.gameObject);

		// Activate all objects in the array
		if (tapWater != null)
		{
			tapWater.SetActive(true);
		}
		// if (sinkWater != null)
		// {
		// 	sinkWater.SetActive(true);
		// }
		hasBeenUsed = true;
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
