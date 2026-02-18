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
        puzzleMaster.PickupFood(foodID);
    }
    public override void Use() {  }
}
