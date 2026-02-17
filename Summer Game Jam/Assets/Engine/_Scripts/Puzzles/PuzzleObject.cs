using UnityEngine;

public class PuzzleObject : MonoBehaviour
{
	[SerializeField] WardrobePuzzle parentPuzzle;
	[SerializeField] int keyIndex = 0;
	Interactable item;

    void HandlePuzzleObject()
    {
		Debug.Log("Puzzle Object Interacted");

		if (parentPuzzle != null)
		{
			parentPuzzle.Validate(keyIndex);
		}
    }
	private void Awake()
	{
		item = this.GetComponent<Interactable>();

		parentPuzzle = GetComponentInParent<WardrobePuzzle>();

		if (parentPuzzle == null)
		{
			Debug.LogWarning("Puzzle Object is not a child of a Puzzle. Please ensure it is placed correctly in the hierarchy.");
		}
	}

	private void OnEnable()
	{
		item.OnInteractedEvent += HandlePuzzleObject;
	}

	private void OnDisable()
	{
		item.OnInteractedEvent -= HandlePuzzleObject;
	}
}
