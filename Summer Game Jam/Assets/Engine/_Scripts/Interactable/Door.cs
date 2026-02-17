using UnityEngine;

public class Door : MonoBehaviour, IInteractable, IInteractDuration
{
	// Components
	Animator animator;

	[Header("Door Settings")]
	[SerializeField] bool isLocked = false;
	[SerializeField] string keyID = "key";
	public float InteractionTime { get; set; } = 0f;

	bool isOpen = false;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public Vector3 GetTargetPoint()
	{
		return transform.position;
	}

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

	public void Use()
	{
		if (!isOpen)
			Open();
		else 
			Close();
	}

	void Open()
	{
		animator.SetTrigger("Open");
		isOpen = true;
	}

	void Close()
	{
		animator.SetTrigger("Close");
		isOpen = false;
	}
}
