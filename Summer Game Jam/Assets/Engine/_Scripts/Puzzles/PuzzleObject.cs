using UnityEngine;

public class PuzzleObject : MonoBehaviour
{
	[SerializeField] Puzzle parentPuzzle;
	[SerializeField] int keyIndex = 0;
	Interactable item;

    void HandlePuzzleObject()
    {
		Debug.Log("Puzzle Object Interacted");

		if (parentPuzzle != null)
		{
			parentPuzzle.ValidatePuzzle(keyIndex);
		}
    }
	private void Awake()
	{
		item = this.GetComponent<Interactable>();

		if (parentPuzzle == null)
			parentPuzzle = GetComponentInParent<Puzzle>();

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

	private void OnDrawGizmos()
	{
		if (parentPuzzle != null)
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine(transform.position, parentPuzzle.transform.position);
			Gizmos.DrawSphere(transform.position, 0.05f);
			Gizmos.color = Color.white;
		}
	}
}
