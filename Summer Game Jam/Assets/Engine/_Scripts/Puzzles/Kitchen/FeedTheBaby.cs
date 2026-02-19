using System.Collections.Generic;
using UnityEngine;

public class FeedTheBaby : Puzzle
{
    public string correctUncookedFoodID;
    public string correctCookedFoodID;
    public int numberOfTries;

    public List<Interactable> foods;
    public Microwave microwave;
    public AudioSource babyScreams;

    public bool foodHeld;
    private string heldFoodID;

    public override void ActivatePuzzle()
    {
        babyScreams.Play();
        base.ActivatePuzzle();
    }

    #region Food Interaction
    public string GetHeldFood()
    {
        if (foodHeld)
        {
            return heldFoodID;
        }
        else return "none";
    }

    public void PickupFood(string foodID)
    {
        if (foodHeld)
        {
            // say no somehow
            return;
        }

        foodHeld = true;

        heldFoodID = foodID;
    }

    public void DropFood()
    {
        foodHeld = false;
        heldFoodID = null;
    }
    #endregion

    public void GiveFoodToBaby()
    {
        if (!foodHeld)
        {
            Debug.Log("No Food");
            // wrong, no food
            return;
        }
        else if (heldFoodID != null && heldFoodID != correctCookedFoodID)
        {
            Debug.Log("Wrong Food");
            // wrong food
            return;
        }

        if (heldFoodID == correctCookedFoodID)
        {
            babyScreams.Stop();
            SolvePuzzle();
        }
    }
}
