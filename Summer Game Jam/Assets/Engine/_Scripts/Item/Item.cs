using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public abstract class Item : MonoBehaviour, IInteractable, IInteractDuration
{
	public float InteractionTime { get; set; } = 0f;
	Animator anim;
	public Vector3 GetTargetPoint()
	{
		return transform.position;
	}
	public abstract void Use();

	public void OnFocus()
	{
		anim?.SetBool("isFocused", true);
		// Debug.Log("Item focused");
	}

	public void OnUnfocus()
	{
		anim?.SetBool("isFocused", false);
		// Debug.Log("Item Unfocused");
		// Code to execute when the door loses focus
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

	private void Awake()
	{
		anim = TryGetComponent<Animator>(out Animator animator) ? animator : null;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}
}