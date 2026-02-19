using System;
using System.Collections.Generic;
using UnityEngine;

public class PlungingPlunger : MonoBehaviour, IInteractable
{
	public float InteractionTime { get; set; } = 0f;
	Animator anim;

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
		Debug.Log("Plunging plunger focused");
	}

	public void OnUnfocus()
	{
		anim?.SetBool("isFocused", false);
		Debug.Log("Plunging plunger Unfocused");
		// Code to execute when the door loses focus
	}

	public void OnInteract(GameObject obj)
	{
		Debug.Log("Interacted with plunging plunger");
		// Use();
        Destroy(this.gameObject);
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
