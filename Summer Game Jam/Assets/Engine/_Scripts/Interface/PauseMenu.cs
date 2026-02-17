using UnityEngine;
using UnityEditor.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }

    [SerializeField] CanvasGroup pauseMenuCanvasGroup;
    bool isPaused = false;

	public void Pause()
    {
        Toggle(!isPaused);
	}

    void Toggle(bool state)
    {
        pauseMenuCanvasGroup.alpha = state ? 1 : 0;
        pauseMenuCanvasGroup.interactable = state;
        pauseMenuCanvasGroup.blocksRaycasts = state;
        isPaused = state;
	}

    public void Resume()
    {
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
