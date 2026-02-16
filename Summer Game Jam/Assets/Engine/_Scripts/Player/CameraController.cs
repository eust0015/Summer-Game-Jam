using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [Header("Components")]
	[SerializeField] Camera mainCamera;
	[SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] CinemachineBrain cinemachineBrain;
	[SerializeField] CinemachinePanTilt cinemachinePanTilt;

	[Header("Settings")]
    [SerializeField] Vector2 cameraSensitivity = new Vector2(0.1f, 2f);
    [SerializeField] float sensitivity = 1.0f;
    [SerializeField] bool lockMouse = true;
    [SerializeField] Vector2 pitchMinMax = new Vector2(-40.0f, 85.0f);
	[SerializeField] Vector2 defaultPanTilt = new Vector2(0.0f, 0.0f);

	float pan;
    float tilt;


	private void Awake()
	{
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

		if (mainCamera == null)
        {
            mainCamera = Camera.main;
		}

		if (Camera.main.GetComponent<CinemachineBrain>() == null)
		{
			Camera.main.gameObject.AddComponent<CinemachineBrain>();
			Debug.Log("CinemachineBrain added to main camera");
		}

		cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();

		if (cinemachineBrain == null)
		{
			Debug.LogError("Camera does not have CineMachine brain reference");
		}
		else
		{
			cinemachineBrain.UpdateMethod = CinemachineBrain.UpdateMethods.SmartUpdate;
			cinemachineBrain.BlendUpdateMethod = CinemachineBrain.BrainUpdateMethods.LateUpdate;
		}
	}

	void ToggleMouse(bool toggle)
	{
		if (toggle)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	void ResetCamera()
	{
		cinemachinePanTilt.PanAxis.Value = defaultPanTilt.x;
		cinemachinePanTilt.TiltAxis.Value = defaultPanTilt.y;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		ToggleMouse(lockMouse);
		ResetCamera();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
