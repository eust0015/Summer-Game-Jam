using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Crosshair : MonoBehaviour
{
    public static Crosshair Instance { get; private set; }
    [SerializeField] private Image crosshairFillImage;
    [SerializeField] private Image crosshairDetectImage;
    [SerializeField] private CanvasGroup crosshairCanvasGroup;

	[SerializeField] Color detectColour;
    [SerializeField] Color defaultColour;

	public void SetProgress(int progress)
    {
        crosshairFillImage.fillAmount = progress / 100f;
	}

    public void SetDetect(bool detected)
    {
        crosshairDetectImage.color = detected ? detectColour : defaultColour;
	}

    public void SetVisible(bool state)
    {
            crosshairCanvasGroup.alpha = state ? 1 : 0;
    }

	private void Awake()
	{
        Instance = this;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        SetProgress(0);
	}
}
