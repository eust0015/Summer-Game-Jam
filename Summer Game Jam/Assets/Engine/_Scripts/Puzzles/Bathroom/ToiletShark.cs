using System;
using System.Collections.Generic;
using UnityEngine;

public class ToiletShark : MonoBehaviour, IInteractable
{
	public float InteractionTime { get; set; } = 0f;
	Animator anim;

	[SerializeField] private GameObject explosion;



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
			Destroy(this.gameObject);			
			if (explosion != null)
			{
				Instantiate(explosion, transform.position, Quaternion.identity);
			}
			hasBeenUsed = true;

		}

		// Use();
        
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