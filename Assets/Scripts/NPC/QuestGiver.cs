using UnityEngine;

public class QuestGiver : BaseNPC, INPC
{
    public void Interact()
    {
        Debug.Log("I have a quest for you!");
        // Add quest initiation logic here (e.g., displaying quest details)
    }

    public string GetNPCType()
    {
        return "QuestGiver";
    }
}
