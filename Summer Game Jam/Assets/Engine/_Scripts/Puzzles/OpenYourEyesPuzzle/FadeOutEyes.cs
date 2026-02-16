using UnityEngine;
using UnityEngine.Events;

public class FadeOutEyes : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDurationInSeconds = 1f;
    [SerializeField] private UnityEvent onFadeOutComplete;

    private void Update()
    {
        canvasGroup.alpha -= Time.deltaTime / fadeDurationInSeconds;
        if (canvasGroup.alpha <= 0)
        {
            canvasGroup.alpha = 0;
            CompleteFadeOut();
        }
    }

    private void CompleteFadeOut()
    {
        onFadeOutComplete?.Invoke();
        enabled = false;
    }
}
