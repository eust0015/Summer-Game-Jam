using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractUI : MonoBehaviour
{
    public static InteractUI Instance { get; private set; }

	[SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _hintText;
    [SerializeField] private CanvasGroup _canvasGroup;

    Interactable _currentInteractable;

    Vector2 defaultTextPosition;
    Vector2 defaultHintTextPosition;

    Vector2 screenCenter;

	public void SetInteractable(Interactable interactable)
    {
        _currentInteractable = interactable;

        SetVisible(true);
        SetText(interactable.ItemID, interactable.PromptText);
	}

    public void ResetInteractable()
    {
        _currentInteractable = null;
        ResetTooltip();
    }

	public void SetVisible(bool visible)
    {
        _canvasGroup.alpha = visible ? 1 : 0;
	}

    public void SetText(string text, string promptText = "")
    {
        _text.text = text;
        _hintText.text = promptText;
	}

	public void ResetTooltip()
	{
		SetVisible(false);
        SetText(string.Empty, string.Empty);

        _text.transform.position = defaultTextPosition;
        _hintText.transform.position = defaultHintTextPosition;
	}

	public Vector2 GetWorldPosition()
	{
		if (_currentInteractable == null)
			return Vector2.zero;

		return Camera.main.WorldToScreenPoint(_currentInteractable.GetTargetPoint());
	}

	private void Awake()
	{
		if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
		}

        Instance = this;

        defaultTextPosition = _text.transform.position;
        defaultHintTextPosition = _hintText.transform.position;

		screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);


		SetText(string.Empty, string.Empty);
	}

    // Update is called once per frame
    void Update()
    {
        if (_currentInteractable != null)
        {
            Vector2 screenPos = GetWorldPosition();
            
            if (screenPos == Vector2.zero)
            {
                this.transform.position = screenCenter;
			}
            else
            {
				_text.transform.position = screenPos;
                _hintText.transform.position = screenPos + new Vector2(0, -20);
			}

		}
	}
}
