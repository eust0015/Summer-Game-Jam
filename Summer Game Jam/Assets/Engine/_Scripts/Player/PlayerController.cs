using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

	[SerializeField] CameraController playerCamera;
    PlayerInput playerInput;

    // Components
    Rigidbody playerRigidbody;
    CapsuleCollider playerCollider;

	private void Awake()
	{
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
		playerRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
