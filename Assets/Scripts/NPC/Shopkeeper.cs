using UnityEngine;

public class ShopKeeper : BaseNPC
{
    public override void Interact(PlayerQuestHandler questHandler)
    {
        // Update the currently assigned quest before interaction
        UpdateAssignedQuest();

        if (assignedQuest != null && !questGiven)
        {
            if (assignedQuest.isConfirmationRequired)
            {
                PromptQuestConfirmation(questHandler);
            }
            else
            {
                GiveQuest(questHandler);
            } 
        }
        else if (assignedQuest != null && questGiven && !assignedQuest.isCompleted)
        {
            ShowQuestInProgressDialogue();
            OpenShop();
        }
        else if (assignedQuest != null && assignedQuest.isCompleted && !assignedQuest.turnedIn)
        {
            ShowQuestTurnInDialogue();
            questHandler.TurnInQuest(assignedQuest, this);
        }
        else
        {
            OpenShop();
        }
    }

    private void OpenShop()
    {
        // Open shop interface, allowing player to buy/sell items
        Debug.Log("Opening shop interface.");
    }

    public override string GetNPCType()
    {
        return "ShopKeeper";
    }
}
