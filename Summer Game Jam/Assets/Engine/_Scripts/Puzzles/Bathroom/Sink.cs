using System;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour, IInteractable
{
	public float InteractionTime { get; set; } = 0f;
	public SharkRepellant sharkRepellant;
	Animator anim;

	[SerializeField] private GameObject plunger, shark;
	[SerializeField] private float plungerMoveDistance = 0.3f;
	[SerializeField] private float plungerMoveDuration = 0.2f;


	private bool ready = false, hasBeenUsed = false;

	public void setReady(bool value)
	{
		ready = value;
		Debug.Log("Sink is now ready: " + ready);
		
		if (transform.childCount > 0)
		{
			BoxCollider col = transform.GetChild(0).GetComponent<BoxCollider>();
			if (col != null)
			{
				col.enabled = value;
			}
		}
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
		Debug.Log("Sink focused");
	}

	public void OnUnfocus()
	{
		anim?.SetBool("isFocused", false);
		Debug.Log("Sink Unfocused");
		// Code to execute when the door loses focus
	}

	public void OnInteract(GameObject obj)
	{
		Debug.Log("Interacted with sink");
		if (plunger != null)
		{
			hasBeenUsed = true;
			plunger.SetActive(true);
			StartCoroutine(MovePlungerRoutine());
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

private System.Collections.IEnumerator MovePlungerRoutine()
{
    Vector3 startPos = plunger.transform.position;
    Vector3 upPos = startPos + Vector3.up * plungerMoveDistance;
	shark.SetActive(true);	
    for (int i = 0; i < 3; i++)
    {
        // Move up
        float elapsed = 0f;
        while (elapsed < plungerMoveDuration)
        {
            plunger.transform.position = Vector3.Lerp(startPos, upPos, elapsed / plungerMoveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        plunger.transform.position = upPos;
        // Move down
        elapsed = 0f;
        while (elapsed < plungerMoveDuration)
        {
            plunger.transform.position = Vector3.Lerp(upPos, startPos, elapsed / plungerMoveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        plunger.transform.position = startPos;
    }
    Destroy(plunger);

    // Find and reverse all RisingWater scripts
    var allWater = GameObject.FindObjectsOfType<RisingWater>();
    foreach (var water in allWater)
    {
        water.ReverseWater();
    }

	if (sharkRepellant != null)
	{
		sharkRepellant.setReady(true);
	}

}
}