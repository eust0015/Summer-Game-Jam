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
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;

        float currentHeight = transform.position.y - startPosition.y;

        if (currentHeight >= deathHeight && !hasTriggeredDeath)
        {
            PlayerDeath();
            hasTriggeredDeath = true;
        }

        if (currentHeight >= targetHeight)
        {
            transform.position = startPosition + Vector3.up * targetHeight;
        }
    }

    private void PlayerDeath()
    {
        Debug.Log("Player Death triggered!");
    }
}
