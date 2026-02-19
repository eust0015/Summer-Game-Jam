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
		Debug.Log("INT FOCUS: " + itemID);
		currentState = FocusState.FOCUSED;
		Crosshair.Instance.SetDetect(true);
		InteractUI.Instance.SetInteractable(this);
		onFocused?.Invoke();
	}

	public void OnUnfocus()
	{
		Debug.Log("INT UNFOCUS: " + itemID);
		currentState = FocusState.UNFOCUSED;
		Crosshair.Instance.SetDetect(false);
		InteractUI.Instance.ResetInteractable();
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
	

	public Vector3 GetTargetPoint() 
	{
		return (targetPointOverride != null) ? targetPointOverride.transform.position : transform.position;
	}

	private void OnDrawGizmosSelected()
	{
		if (targetPointOverride != null)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(targetPointOverride.position, 0.1f);
			Gizmos.DrawLine(transform.position, targetPointOverride.position);
		}
		else
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(transform.position, 0.1f);
		}
#if UNITY_EDITOR
		Handles.Label(transform.position + Vector3.up * 0.1f, $"Interactable: {itemID} State: {currentState}");
#endif
	}
}