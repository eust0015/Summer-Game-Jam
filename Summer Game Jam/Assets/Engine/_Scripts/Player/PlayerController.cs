using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

	[SerializeField] CameraController playerCamera;
    PlayerInput playerInput;

    // Components
    Rigidbody rb;
    CapsuleCollider playerCollider;

	[Header("Movement Config")]
	[SerializeField] float walkSpeed = 5f;
	[SerializeField] float runSpeed = 10f;
	[SerializeField] float decay = 0.5f;

	Vector2 movementInput { get; set; } = Vector2.zero;

	private void Awake()
	{
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
		rb = GetComponent<Rigidbody>();
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

	private void FixedUpdate()
	{
		ApplyForces();
	}

	void ApplyForces()
	{
		if (movementInput != Vector2.zero)
		{
			Vector3 moveDirection = Camera.main.transform.forward * movementInput.y +
									Camera.main.transform.right * movementInput.x;

			moveDirection.y = 0f;

			float targetSpeed = walkSpeed;
			Vector3 targetVelocity = moveDirection.normalized * targetSpeed;

			Vector3 currentVelocity = rb.linearVelocity;
			Vector3 horizontalVelocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);
			Vector3 deltaV = targetVelocity - horizontalVelocity;

			// keep vertical unchanged, only adjust XZ
			rb.AddForce(deltaV, ForceMode.VelocityChange);
		}
		else
		{
			// Only apply drag to horizontal velocity, not vertical (gravity/fly)
			Vector3 currentVelocity = rb.linearVelocity;
			Vector3 horizontalVelocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);
			Vector3 decayedVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, decay * Time.fixedDeltaTime);
			rb.linearVelocity = new Vector3(decayedVelocity.x, currentVelocity.y, decayedVelocity.z);
		}
	}

	private void OnEnable()
	{
		playerInput.actions["Move"].performed += ctx => movementInput = ctx.ReadValue<Vector2>();
		playerInput.actions["Move"].canceled += ctx => movementInput = ctx.ReadValue<Vector2>();

	}

	private void OnDisable()
	{
		playerInput.actions["Move"].performed -= ctx => movementInput = ctx.ReadValue<Vector2>();
		playerInput.actions["Move"].canceled -= ctx => movementInput = ctx.ReadValue<Vector2>();
	}
}
