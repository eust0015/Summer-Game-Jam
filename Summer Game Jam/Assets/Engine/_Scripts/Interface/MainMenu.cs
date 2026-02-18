using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        Debug.Log("Play game button clicked");
        SceneManager.LoadSceneAsync(1);
	}

    public void Credits()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
