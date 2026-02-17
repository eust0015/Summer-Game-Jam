using UnityEngine;
using System;

public class Puzzle : MonoBehaviour
{
    public event Action<Puzzle> OnPuzzleSolved;

	[SerializeField] PuzzleData puzzleData;

	public void SolvePuzzle()
    {
        Debug.Log("Puzzle Solved");
        puzzleData.isSolved = true;
        OnPuzzleSolved?.Invoke(this);
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

[System.Serializable]
public class PuzzleData
{
    public string puzzleName;
    public bool isSolved;
}
