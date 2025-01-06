using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestHandler : MonoBehaviour
{
    public List<Quest> activeQuests = new List<Quest>();

    private void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.playerQuest = this;
        }
    }

    public void AddQuest(Quest newQuest)
    {
        if (newQuest == null)
        {
            return;
        }
        if (!activeQuests.Contains(newQuest))
        {
            activeQuests.Add(newQuest);

            if (GameManager.instance.playerQuest)
            {
                GameManager.instance.playerQuest.CheckQuestCompletionConditions();
            }

            UpdateQuestDisplay();
        }
    }

    public void CompleteQuest(Quest quest)
    {
        if (activeQuests.Contains(quest))
        {
            quest.isCompleted = true;

            UpdateQuestDisplay();
        }
    }

    public bool HasQuest(Quest quest)
    {
        return activeQuests.Contains(quest);
    }

    public bool HasItem(Item item, int requiredAmount = 1)
    {
        InventoryItem inventoryItem = Inventory.instance.FindItem(item);
        UpdateQuestDisplay();
        return inventoryItem != null && inventoryItem.count >= requiredAmount;
    }

    public void CheckQuestCompletionConditions()
    {
        if(activeQuests.Count == 0)
        {
            return;
        }

        foreach (Quest quest in activeQuests)
        {
            if (quest == null || quest.isCompleted)
            {
                continue;  // Skip null conditions and move to the next one
            }

            if (!quest.isCompleted && quest.AreCompletionConditionsMet(this))
            {
                CompleteQuest(quest);  // Mark the quest as complete if conditions are met
            }
        }
    }

    public void RemoveQuest(Quest quest)
    {
        if (activeQuests.Contains(quest))
        {
            activeQuests.Remove(quest);

            UpdateQuestDisplay();
        }
    }

    // New function to turn in a completed quest to an NPC
    public bool TurnInQuest(Quest quest, BaseNPC npc)
    {
        if (quest.isCompleted && HasQuest(quest))
        {
            quest.turnedIn = true;
            RemoveQuest(quest);
            npc.GiveReward(quest); 
            return true;
        }
        return false;
    }

    // DEBUG FUNCTIONS
    public void CompleteAllActiveQuests()
    {
        foreach (Quest quest in activeQuests) 
        {
            CompleteQuest(quest);
        }
    }

    public void UpdateQuestDisplay()
    {
        if (GameManager.instance.UIHandler != null && GameManager.instance.UIHandler.questUI != null)
        {
            GameManager.instance.UIHandler.questUI.UpdateDisplay(ref activeQuests);
        }
    }
}
