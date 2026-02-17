using UnityEngine;
using System.Collections.Generic;

public class WardrobePuzzle : Puzzle
{
    [SerializeField] List<PuzzleObject> items = new List<PuzzleObject>();
    int randomIndex;

	public void Validate(int key)
	{
		if (key == randomIndex)
		{
			Debug.Log("Correct item selected! Puzzle Solved.");
			SolvePuzzle();
		}
		else
		{
			Debug.Log("Incorrect item. Try again.");
		}
	}


	private void Awake()
	{
		randomIndex = Random.Range(0, items.Count);
		Debug.Log($"Wardrobe Puzzle initialized with random index: {randomIndex}");
	}
}
