using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("KillPlayer triggered by " + other.gameObject.name);
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null && other.transform.parent != null)
        {
            player = other.transform.parent.GetComponent<PlayerController>();
        }
        if (player != null)
        {
            Debug.Log("Player has been killed by " + gameObject.name);
            player.PlayerDeath();
        }
    }
}
