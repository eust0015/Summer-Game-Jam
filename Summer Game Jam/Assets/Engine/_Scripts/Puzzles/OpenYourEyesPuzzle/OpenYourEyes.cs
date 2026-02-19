using UnityEngine;
using UnityEngine.Events;

public class OpenYourEyes : Puzzle
{
    [SerializeField] private RectTransform topEyeLid;
    [SerializeField] private RectTransform bottomEyeLid;
    [SerializeField] private GameObject innerMonologue;
    [SerializeField] private GameObject inputPrompt;
    [SerializeField] private UnityEvent onPuzzleCompleted;
    [SerializeField] private float durationInSeconds = 3f;
    [SerializeField] private AnimationCurve ease;
    [SerializeField] private GameObject mainCamera;
    private static readonly Vector2 topEyeLidClosedPosition = new(0,  270f);
    private static readonly Vector2 topEyeLidOpenPosition = new(0,  500f);
    private static readonly Vector2 bottomEyeLidClosedPosition = new(0, -270f);
    private static readonly Vector2 bottomEyeLidOpenPosition = new(0, -500f);

    private float progress; // 0 to 1
    private bool completed;

    private void Update()
    {
        bool inputIsDown = Input.GetKey(KeyCode.E) || Input.GetMouseButton(0);
        float targetProgress = inputIsDown ? 1f : 0f;

        float step = (durationInSeconds > 0f) ? Time.deltaTime / durationInSeconds : 1f;

        if (targetProgress > progress)
        {
            progress = Mathf.Min(1f, progress + step);
        }
        else if (targetProgress < progress)
            progress = Mathf.Max(0f, progress - step);

        float time = ease.Evaluate(progress);

        topEyeLid.anchoredPosition = Vector2.Lerp(topEyeLidClosedPosition, topEyeLidOpenPosition, time);
        bottomEyeLid.anchoredPosition = Vector2.Lerp(bottomEyeLidClosedPosition, bottomEyeLidOpenPosition, time);

        if (!completed && progress >= 1f)
        {
            CompleteNow();
        }
    }

    public override void ActivatePuzzle()
    {
        Debug.Log("Open eyes puzzle activated");
        completed = false;
        progress = 0f;
        topEyeLid.anchoredPosition = topEyeLidClosedPosition;
        bottomEyeLid.anchoredPosition = bottomEyeLidClosedPosition;
        topEyeLid.gameObject.SetActive(true);
        bottomEyeLid.gameObject.SetActive(true);
        innerMonologue.SetActive(true);
        inputPrompt.SetActive(true);
        mainCamera.SetActive(true);
        enabled = true;

		CameraController.Instance.ToggleCamera(false);
        PlayerTargetSystem.Instance.SetEnabled(false);
		base.ActivatePuzzle();
	}

    private void CompleteNow()
    {
        completed = true;
        topEyeLid.anchoredPosition = topEyeLidOpenPosition;
        bottomEyeLid.anchoredPosition = bottomEyeLidOpenPosition;
        topEyeLid.gameObject.SetActive(false);
        bottomEyeLid.gameObject.SetActive(false);
        onPuzzleCompleted?.Invoke();
		CameraController.Instance.ToggleMouse(false);
		enabled = false;
        //GameManager.Instance.EnableControl(true);
	}
}
