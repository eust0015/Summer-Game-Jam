using UnityEngine;
using UnityEngine.Events;

public class OpenYourEyes : Puzzle
{
    [SerializeField] private RectTransform topEyeLid;
    [SerializeField] private RectTransform bottomEyeLid;
    [SerializeField] private GameObject innerMonologue;
    [SerializeField] private GameObject inputPrompt;
    [SerializeField] private UnityEvent onPuzzleCompleted;
    private const float speedInPixelsPerSecond = 1f;
    private const float durationInSeconds = 3f;
    private static readonly Vector2 topEyeLidClosedPosition = new(0, 270f);
    private static readonly Vector2 topEyeLidOpenPosition = new(0, 500f);
    private static readonly Vector2 bottomEyeLidClosedPosition = new(0, -270f);
    private static readonly Vector2 bottomEyeLidOpenPosition = new(0, -500f);
    private Vector2 topEyeLidVelocity;
    private Vector2 bottomEyeLidVelocity;

    private void Update()
    {
        if (Input.GetKey(KeyCode.F) || Input.GetMouseButton(0))
        {
            OpenEyes();
        }
        else
        {
            CloseEyes();
        }
    }

    private void OpenEyes()
    {
        topEyeLid.anchoredPosition = Vector2.SmoothDamp(topEyeLid.anchoredPosition, topEyeLidOpenPosition, ref topEyeLidVelocity, durationInSeconds);
        bottomEyeLid.anchoredPosition = Vector2.SmoothDamp(bottomEyeLid.anchoredPosition, bottomEyeLidOpenPosition, ref bottomEyeLidVelocity, durationInSeconds);
        innerMonologue.SetActive(false);

        if (Vector2.Distance(topEyeLid.anchoredPosition, topEyeLidOpenPosition) < 0.5f)
        {
            PuzzleCompleted();
        }
    }

    private void CloseEyes()
    {
        topEyeLid.anchoredPosition = Vector2.SmoothDamp(topEyeLid.anchoredPosition, topEyeLidClosedPosition, ref topEyeLidVelocity, durationInSeconds);
        bottomEyeLid.anchoredPosition = Vector2.SmoothDamp(bottomEyeLid.anchoredPosition, bottomEyeLidClosedPosition, ref bottomEyeLidVelocity, durationInSeconds);
        innerMonologue.SetActive(true);
    }

    public override void ActivatePuzzle()
    {
        base.ActivatePuzzle();
        topEyeLid.anchoredPosition = topEyeLidClosedPosition;
        bottomEyeLid.anchoredPosition = bottomEyeLidClosedPosition;
        innerMonologue.SetActive(true);
        inputPrompt.SetActive(true);
        enabled = true;
    }

    private void PuzzleCompleted()
    {
        onPuzzleCompleted?.Invoke();
        enabled = false;
        topEyeLid.gameObject.SetActive(false);
        bottomEyeLid.gameObject.SetActive(false);
        inputPrompt.SetActive(false);
        SolvePuzzle();
    }
}
