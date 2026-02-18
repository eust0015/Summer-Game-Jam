using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
	}

    public void EnableControl(bool state)
    {
        PlayerController.Instance.EnableControl(state);
        Crosshair.Instance.SetVisible(state);
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        PlayerController.Instance.EnableControl(false);
        Crosshair.Instance.SetVisible(false);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
