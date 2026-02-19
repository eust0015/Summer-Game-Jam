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
	bool isControlEnabled = true;

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

	public void EnableControl(bool state)
	{
		isControlEnabled = state;
		CameraController.Instance.ToggleCameraMovement(state);
	}

	public void PlayerDeath()
	{
		EnableControl(false);

		if (youDiedCanvas != null)
		{
			youDiedCanvas.enabled = true;
		}

		



		if (cameraTransform != null)
		{
			cameraTransform.parent = null;
			Rigidbody camRb = GetComponent<Rigidbody>();
			if (camRb == null)
			{
				camRb = cameraTransform.gameObject.AddComponent<Rigidbody>();
			}
			
			camRb.isKinematic = false;
			camRb.useGravity = true;
			camRb.AddForce(Vector3.down * 10f, ForceMode.Impulse);
			camRb.AddForce(Vector3.left * 10f, ForceMode.Impulse);
			camRb.AddTorque(new Vector3(Random.Range(-200, 200), Random.Range(-200, 200), Random.Range(-200, 200)), ForceMode.Impulse);
			// Debug.DrawLine(this.transform.position, , Color.red, 2f);
			Debug.Log("Applied force and torque to camera for death effect.");
		}

		Debug.Log("Player has died.");
		GetComponent<Rigidbody>().freezeRotation = false;
		GetComponent<PlayerController>().enabled = false;

	}

	private void FixedUpdate()
	{
		if (isControlEnabled)
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

	[SerializeField] private Transform cameraTransform;
	[SerializeField] private Canvas youDiedCanvas;
}
