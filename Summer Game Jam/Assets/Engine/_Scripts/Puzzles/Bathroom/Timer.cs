using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float countdownTime = 15f;
    private float timer;
    private TextMeshProUGUI textMesh;
    private bool hasTriggeredDeath = false;

    void Start()
    {
        timer = countdownTime;
        textMesh = GetComponent<TextMeshProUGUI>();
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (hasTriggeredDeath) return;
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer < 0f) timer = 0f;
            UpdateTimerDisplay();
            if (timer == 0f)
            {
                TriggerPlayerDeath();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.FloorToInt(timer);
        int minutes = seconds / 60;
        int hours = minutes / 60;
        seconds = seconds % 60;
        minutes = minutes % 60;
        if (textMesh != null)
            textMesh.text = $"{hours:00}:{minutes:00}:{seconds:00}";
    }

    void TriggerPlayerDeath()
    {
        hasTriggeredDeath = true;
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.PlayerDeath();
        }
    }
}
