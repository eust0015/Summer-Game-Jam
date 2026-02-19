using UnityEngine;
using UnityEngine.Events;

public class GetOutOfBed : Puzzle
{
    [SerializeField] private GetOutOfBedTarget target1;
    [SerializeField] private GetOutOfBedTarget target2;
    [SerializeField] private GameObject innerMonologue;
    [SerializeField] private UnityEvent onPuzzleCompleted;

    public override void ActivatePuzzle()
    {
        target1.ActivateTarget();
        innerMonologue.SetActive(true);
        base.ActivatePuzzle();
    }

    public void PuzzleCompleted()
    {
        innerMonologue.SetActive(false);
        onPuzzleCompleted?.Invoke();
        SolvePuzzle();
    }
}
