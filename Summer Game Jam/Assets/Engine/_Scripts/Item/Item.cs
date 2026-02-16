using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public abstract class Item : MonoBehaviour, IInteractable, IInteractDuration
{
	public float InteractionTime { get; set; } = 0f;
	public Vector3 GetTargetPoint()
	{
		return transform.position;
	}
	public abstract void Use();

	public void OnFocus()
	{
		// Code to execute when the door is focused
		Debug.Log("Item focused");
	}

	public void OnUnfocus()
	{
		// Code to execute when the door loses focus
		Debug.Log("Item lost focus");
	}

	public void OnInteract(GameObject obj)
	{
		Debug.Log("Interacted with item");
		Use();
	}

	public void Drop()
	{
		// Implement drop logic here
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}