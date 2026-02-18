using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }


	[SerializeField] Puzzle FirstPuzzle;

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
        if (FirstPuzzle != null)
        {
            FirstPuzzle.ActivatePuzzle();
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
