using UnityEngine;
using UnityEngine.Events;

public class OpenYourEyes : Puzzle
{
    [SerializeField] private RectTransform topEyeLid;
    [SerializeField] private RectTransform bottomEyeLid;
    [SerializeField] private GameObject innerMonologue;
    [SerializeField] private UnityEvent onPuzzleCompleted;
    private const float speedInPixelsPerSecond = 1f;
    private static readonly Vector2 topEyeLidClosedPosition = new(0, 270f);
    private static readonly Vector2 topEyeLidOpenPosition = new(0, 500f);
    private static readonly Vector2 bottomEyeLidClosedPosition = new(0, -270f);
    private static readonly Vector2 bottomEyeLidOpenPosition = new(0, -500f);

    private void Update()
    {
        if (Input.GetMouseButton(0))
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
        topEyeLid.anchoredPosition = Vector2.Lerp(topEyeLid.anchoredPosition, topEyeLidOpenPosition, Time.deltaTime * speedInPixelsPerSecond);
        bottomEyeLid.anchoredPosition = Vector2.Lerp(bottomEyeLid.anchoredPosition, bottomEyeLidOpenPosition, Time.deltaTime * speedInPixelsPerSecond);
        innerMonologue.SetActive(false);

        if (Vector2.Distance(topEyeLid.anchoredPosition, topEyeLidOpenPosition) < 0.1f)
        {
            PuzzleCompleted();
        }
    }

    private void CloseEyes()
    {
        topEyeLid.anchoredPosition = Vector2.Lerp(topEyeLid.anchoredPosition, topEyeLidClosedPosition, Time.deltaTime * speedInPixelsPerSecond);
        bottomEyeLid.anchoredPosition = Vector2.Lerp(bottomEyeLid.anchoredPosition, bottomEyeLidClosedPosition, Time.deltaTime * speedInPixelsPerSecond);
        innerMonologue.SetActive(true);
    }

    private void PuzzleCompleted()
    {
        onPuzzleCompleted?.Invoke();
        enabled = false;
        GameManager.Instance.EnableControl(true);
	}
}
