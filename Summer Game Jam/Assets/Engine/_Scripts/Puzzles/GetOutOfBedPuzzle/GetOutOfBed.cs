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
		CameraController.Instance.ToggleMouse(false);
		innerMonologue.SetActive(true);
		base.ActivatePuzzle();
    }

    public void PuzzleCompleted()
    {
        innerMonologue.SetActive(false);
        onPuzzleCompleted?.Invoke();
        GameManager.Instance?.EnableControl(true);
		CameraController.Instance?.ToggleCamera(true);
		CameraController.Instance?.ToggleMouse(true);
		PlayerTargetSystem.Instance?.SetEnabled(true);
		SolvePuzzle(true);
    }
}
