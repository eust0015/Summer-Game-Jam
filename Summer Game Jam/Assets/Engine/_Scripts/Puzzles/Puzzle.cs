using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour
{
    [Header("Puzzle Config")]
    [SerializeField] string puzzleName = "Unnamed Puzzle";
    [SerializeField] bool isPuzzleSolved = false;
    [SerializeField] int requiredIndex = 0;

	public event Action<Puzzle> OnPuzzleSolved;
	public UnityEvent PuzzleSolvedAction;

	public Puzzle NextPuzzle;
    public bool isActive = false;

    [SerializeField] GameObject nextPuzzle;

    IEnumerator NextPuzzleRoutine()
    {
        yield return new WaitForSeconds(2f);

		if (NextPuzzle != null)
		{
			Debug.Log($"Activating next puzzle: {NextPuzzle.puzzleName}");
			NextPuzzle.ActivatePuzzle();
		}
		else
		{
			Debug.Log("No next puzzle to activate.");
		}

		if (nextPuzzle != null)
		{
			nextPuzzle.SetActive(true);
		}
	}

    public virtual void ActivatePuzzle()
    {
        isActive = true;
        Debug.Log($"Activating puzzle: {puzzleName}");
	}

    public virtual void ValidatePuzzle(int index)
    {
        Debug.Log($"{puzzleName}:{isActive} received index: {index}");

        if (isActive)
        {
            if (index == requiredIndex)
            {
                Debug.Log($"Correct index {index} for puzzle: {puzzleName}");
                SolvePuzzle();
            }
            else
            {
                Debug.Log($"Incorrect index {index} for puzzle: {puzzleName}. Required index is {requiredIndex}");
			}
		}
    }

    public void SolvePuzzle()
    {
        Debug.Log("Puzzle Solved");
        isPuzzleSolved = true;
        OnPuzzleSolved?.Invoke(this);
        PuzzleSolvedAction?.Invoke();

        if (this.enabled)
            StartCoroutine(NextPuzzleRoutine());
    }

	private void OnDrawGizmos()
	{
#if UNITY_EDITOR
        Handles.Label(transform.position + Vector3.up * 0.1f, $"Puzzle: {puzzleName} State: {isPuzzleSolved}");
#endif
        if (NextPuzzle != null)
        {
            Gizmos.color = (isPuzzleSolved) ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, NextPuzzle.transform.position);
		}
	}
}

[System.Serializable]
public class PuzzleData
{
    public string puzzleName;
    public bool isSolved;
}
