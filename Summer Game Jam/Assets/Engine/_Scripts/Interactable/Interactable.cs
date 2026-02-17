using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

public interface IInteractable
{
	void OnFocus();
	void OnUnfocus();
	void OnInteract(GameObject interactor);

	void InteractProgress(float progress) { }

	Vector3 GetTargetPoint();
}


[DisallowMultipleComponent]
public class Interactable : MonoBehaviour, IInteractable, IInteractDuration
{
	public enum FocusState
	{
		NONE,
		FOCUSED,
		UNFOCUSED
	}

	[Header("Interactable Config")]
	[SerializeField] private string itemID = "unset";
	[SerializeField] private string promptText = "";
	[SerializeField] private float interactionTime = 0.5f;

	public float InteractionTime { get; set; } = 0.5f;
	[SerializeField] private Transform targetPointOverride;

	public UnityEvent onFocused;
	public UnityEvent onUnfocused;
	public UnityEvent<GameObject> onInteracted;

	public event Action OnInteractedEvent;


	public string ItemID => itemID;
	public string PromptText => promptText;
	private FocusState currentState = FocusState.NONE;

	private void Awake()
	{
		InteractionTime = interactionTime;
	}

	public void OnFocus()
	{
		currentState = FocusState.FOCUSED;
		Crosshair.Instance.SetDetect(true);
		onFocused?.Invoke();
	}

	public void OnUnfocus()
	{
		currentState = FocusState.UNFOCUSED;
		Crosshair.Instance.SetDetect(false);
		onUnfocused?.Invoke();
	}

	public void InteractProgress(float progress)
	{
		Crosshair.Instance.SetProgress((int)(progress * 100f));
	}

	public void OnInteract(GameObject interactor)
	{
		Debug.Log("Interacted with " + itemID);
		OnInteractedEvent?.Invoke();
		onInteracted?.Invoke(interactor);
	}
	

	public Vector3 GetTargetPoint() => transform.position;

	private void OnDrawGizmosSelected()
	{
		Handles.Label(transform.position + Vector3.up * 0.1f, $"Interactable: {itemID} State: {currentState}");
	}
}