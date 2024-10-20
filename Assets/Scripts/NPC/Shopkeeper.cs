using UnityEngine;

public class Shopkeeper : BaseNPC, INPC
{
    private bool hasGreeted = false;

    public void Interact()
    {
        if (!hasGreeted)
        {
            Debug.Log("Welcome to my shop!");
            hasGreeted = true;
        }
        else
        {
            Debug.Log("What would you like to buy?");
        }
    }

    public string GetNPCType()
    {
        return "Shopkeeper";
    }
}
