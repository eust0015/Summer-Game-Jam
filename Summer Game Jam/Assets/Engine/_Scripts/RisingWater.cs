using UnityEngine;

public class RisingWater : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 1f;
    [SerializeField] private float targetHeight = 10f;
    [SerializeField] private float deathHeight = 15f;
    
    private Vector3 startPosition;
    private bool hasTriggeredDeath = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Raise the object slowly
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;

        float currentHeight = transform.position.y - startPosition.y;

        // Check if object has surpassed the death height
        if (currentHeight >= deathHeight && !hasTriggeredDeath)
        {
            PlayerDeath();
            hasTriggeredDeath = true;
        }

        // Stop rising at target height
        if (currentHeight >= targetHeight)
        {
            transform.position = startPosition + Vector3.up * targetHeight;
        }
    }

    private void PlayerDeath()
    {
        Debug.Log("Player Death triggered!");
        // Add player death logic here
    }
}
