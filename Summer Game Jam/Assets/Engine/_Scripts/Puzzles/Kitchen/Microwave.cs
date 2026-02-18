using UnityEngine;

public class Microwave : MonoBehaviour
{
    public GameObject internalInteractionPoint;
    public GameObject bowl;
    public AudioClip normalRunningAudio;
    public AudioClip badRunningAudio;
    public AudioClip explodeAudio;
    public AudioClip beepingAudio;
    private int remainingTries;
    private FeedTheBaby puzzleMaster;
    private Animator animator;
    private Door door;
    private AudioSource audioSource;

    void Awake()
    {
        puzzleMaster = GetComponentInParent<FeedTheBaby>();
        remainingTries = puzzleMaster.numberOfTries;
        animator = GetComponent<Animator>();
        door = GetComponentInChildren<Door>();
        audioSource = GetComponent<AudioSource>();
    }

    public void StartMicrowave()
    {
        if (puzzleMaster.GetHeldFood() != null) 
        {
            if (puzzleMaster.GetHeldFood() == puzzleMaster.correctUncookedFoodID)
            {
                Debug.Log("Microwave Running Good");
                animator.SetTrigger("RunNormal");
            }
            else if (remainingTries <= 0)
            {
                Debug.Log("Microwave Running Bad");
                animator.SetTrigger("Explode");
            }
            else
            {
                Debug.Log("Microwave Exploding");
                animator.SetTrigger("RunBad");
                remainingTries--;
            }
            puzzleMaster.DropFood();
        }
    }

    #region Animation Triggers
    public void EndMicrowaveNormal()
    {
        Debug.Log("Microwave Done");
        audioSource.Stop();
        audioSource.PlayOneShot(beepingAudio);
        ToggleDoor();
    }

    public void EndMicrowaveBad()
    {
        Debug.Log("Microwave Done");
        internalInteractionPoint.SetActive(true);
        bowl.SetActive(false);
        audioSource.Stop();
        audioSource.PlayOneShot(beepingAudio);
        ToggleDoor();
    }

    public void Explode()
    {
        Debug.Log("Microwave Exploded");
        audioSource.PlayOneShot(explodeAudio);
        ToggleDoor();
    }

    public void RunMicrowaveNormal()
    {
        Debug.Log("Microwave Started");
        bowl.SetActive(true);
        internalInteractionPoint.SetActive(false);
        audioSource.PlayOneShot(normalRunningAudio);
        ToggleDoor();
    }

    public void RunMicrowaveBad()
    {
        bowl.SetActive(true);
        internalInteractionPoint.SetActive(false);
        audioSource.PlayOneShot(badRunningAudio);
        ToggleDoor();
    }

    public void MicrowaveExplode()
    {
        bowl.SetActive(true);
        internalInteractionPoint.SetActive(false);
        audioSource.PlayOneShot(badRunningAudio);
        ToggleDoor();
    }

    public void ToggleDoor()
    {
        door.Use();
    }
    #endregion
}
