using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler Instance { get; private set; }

	PlayerInput playerInput;

    public Vector2 MovementInput { get; private set; } = Vector2.zero;
	public float InteractInput { get; private set; } = 0f;

	private void Awake()
	{
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
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

	private void OnEnable()
	{
		playerInput.actions["Move"].performed += ctx => MovementInput = ctx.ReadValue<Vector2>();
		playerInput.actions["Move"].canceled += ctx => MovementInput = ctx.ReadValue<Vector2>();
		playerInput.actions["Interact"].performed += ctx => InteractInput = ctx.ReadValue<float>();
		playerInput.actions["Interact"].canceled += ctx => InteractInput = ctx.ReadValue<float>();
	}

	private void OnDisable()
	{
		playerInput.actions["Move"].performed -= ctx => MovementInput = ctx.ReadValue<Vector2>();
		playerInput.actions["Move"].canceled -= ctx => MovementInput = ctx.ReadValue<Vector2>();
		playerInput.actions["Interact"].performed -= ctx => InteractInput = ctx.ReadValue<float>();
		playerInput.actions["Interact"].canceled -= ctx => InteractInput = ctx.ReadValue<float>();
	}
}
