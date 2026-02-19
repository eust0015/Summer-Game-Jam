using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GetOutOfBedTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float durationInSeconds = 3f;
    [SerializeField] private AnimationCurve ease;
    [SerializeField] private Vector2 startPosition = new(0f, 100f);
    [SerializeField] private Vector2 endPosition = new(0f, 980f);
    [SerializeField] private Vector3 startScale = Vector3.one;
    [SerializeField] private Vector3 endScale = new(0.25f, 0.25f, 0.25f);
    [SerializeField] private Vector3 startRotationInDegrees = new(270f, 0f, 0f);
    [SerializeField] private Vector3 endRotationInDegrees = new(360f, 0f, 0f);
    [SerializeField] private UnityEvent onDestinationReached;
    private bool mouseIsOver;
    private bool completed;
    private float progress; // 0 to 1

    private void Update()
    {
        float progressTarget = mouseIsOver ? 1f : 0f;

        float step = (durationInSeconds > 0f) ? Time.deltaTime / durationInSeconds : 1f;
        if (progressTarget > progress)
        {
            progress = Mathf.Min(1f, progress + step);
        }
        else if (progressTarget < progress)
        {
            progress = Mathf.Max(0f, progress - step);
        }

        float t = ease.Evaluate(progress);

        rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
        rectTransform.localScale = Vector3.Lerp(startScale, endScale, t);
        Vector3 euler = LerpEulerAngles(startRotationInDegrees, endRotationInDegrees, t);
        mainCamera.localRotation = Quaternion.Euler(euler);

        if (!completed && progress >= 1f)
        {
            DestinationReached();
        }
    }

    public void ActivateTarget()
    {
        completed = false;
        progress = 0f;
        rectTransform.anchoredPosition = startPosition;
        rectTransform.localScale = startScale;
        mainCamera.localRotation = Quaternion.Euler(startRotationInDegrees);
        gameObject.SetActive(true);
    }

    private void DestinationReached()
    {
        completed = true;
        rectTransform.anchoredPosition = endPosition;
        rectTransform.localScale = endScale;
        mainCamera.localRotation = Quaternion.Euler(endRotationInDegrees);
		onDestinationReached?.Invoke();
        gameObject.SetActive(false);
    }

	public void OnPointerEnter(PointerEventData eventData) => mouseIsOver = true;
    public void OnPointerExit(PointerEventData eventData)  => mouseIsOver = false;

    private static Vector3 LerpEulerAngles(Vector3 startRotation, Vector3 endRotation, float t)
    {
        return new Vector3(
            Mathf.LerpAngle(startRotation.x, endRotation.x, t),
            Mathf.LerpAngle(startRotation.y, endRotation.y, t),
            Mathf.LerpAngle(startRotation.z, endRotation.z, t)
        );
    }
}
