using UnityEngine;

public class RisingWater : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 1f;
    [SerializeField] private float targetHeight = 10f;
    [SerializeField] private float deathHeight = 15f;
    [SerializeField] private GameObject objectToEnable;
    
    private Vector3 startPosition;
    private bool hasTriggeredDeath = false;
    private bool hasTriggeredTargetHeight = false;
    private bool isReversing = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float currentHeight = transform.position.y - startPosition.y;

        if (isReversing)
        {
            if (currentHeight > 0f)
            {
                transform.position -= Vector3.up * riseSpeed * Time.deltaTime;
                if (transform.position.y < startPosition.y)
                    transform.position = startPosition;
            }
            else
            {
                Destroy(gameObject);
            }
            return;
        }

        if (currentHeight < targetHeight)
        {
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
            // Debug.Log($"Rising water height: {currentHeight:F2}");
        }

        currentHeight = transform.position.y - startPosition.y;

        if (currentHeight >= deathHeight && !hasTriggeredDeath)
        {
            PlayerDeath();
            hasTriggeredDeath = true;
        }

        if (currentHeight >= targetHeight && !hasTriggeredTargetHeight)
        {
            transform.position = startPosition + Vector3.up * targetHeight;
            
            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true);
            }
            
            hasTriggeredTargetHeight = true;
        }
    }

    private void PlayerDeath()
    {
        Debug.Log("Player Death triggered!");
        var player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.PlayerDeath();
        }
    }

    public void ReverseWater()
    {
        isReversing = true;
    }
}
