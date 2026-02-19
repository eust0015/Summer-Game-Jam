using UnityEngine;

public class Food : Item, IInteractable
{
    public string foodID;
    private FeedTheBaby puzzleMaster;
    void Awake()
    {
        puzzleMaster = GetComponentInParent<FeedTheBaby>();
    }
    public void Pickup()
    {
        Debug.Log($"Picked Up {foodID}");
        if (puzzleMaster.PickupFood(foodID))
        {
            this.gameObject.SetActive(false);
        }
    }
    public override void Use() {  }
}
