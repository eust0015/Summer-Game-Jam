using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }

    [SerializeField] CanvasGroup pauseMenuCanvasGroup;
    bool isPaused = false;

	public void Pause()
    {
        Toggle(!isPaused);
		PlayerController.Instance.EnableControl(!isPaused);
	}

    void Toggle(bool state)
    {
        pauseMenuCanvasGroup.alpha = state ? 1 : 0;
        pauseMenuCanvasGroup.interactable = state;
        pauseMenuCanvasGroup.blocksRaycasts = state;
        isPaused = state;

        CameraController.Instance.ToggleMouse(true);
	}

    public void Resume()
    {
        Debug.Log("CLICKED RESUME");
		PlayerController.Instance.EnableControl(true);
		Toggle(false);
	}

    public void Quit()
    {
		SceneManager.LoadSceneAsync(0);
    }

	private void Awake()
	{
		if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        Toggle(false);
	}
}
