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
        else
        {
            Debug.LogWarning("GameManager instance is null. PlayerQuestHandler could not initialize.");
        }
    }

    public void AddQuest(Quest newQuest)
    {
        if (newQuest == null)
        {
            Debug.LogWarning("Attempted to add a null quest.");
            return;
        }
        if (!activeQuests.Contains(newQuest))
        {
            activeQuests.Add(newQuest);
            Debug.Log($"Quest added: {newQuest.questName}");

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
            Debug.Log($"Quest completed: {quest.questName}");

            UpdateQuestDisplay();
        }
    }

    public bool HasQuest(Quest quest)
    {
        return activeQuests.Contains(quest);
    }

    public void CheckQuestCompletionConditions()
    {
        if(activeQuests.Count == 0)
        {
            return;
        }

        foreach (Quest quest in activeQuests)
        {
            if (quest == null)
            {
                Debug.LogWarning($"A quest is null in {quest.name}.");
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
            Debug.Log($"Quest removed: {quest.questName}");

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
        Debug.Log("Quest is not completed or does not exist in active quests.");
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

    private void UpdateQuestDisplay()
    {
        if (GameManager.instance.UIHandler != null && GameManager.instance.UIHandler.questUI != null)
        {
            GameManager.instance.UIHandler.questUI.UpdateDisplay(ref activeQuests);
        }
        else
        {
            Debug.LogWarning("UIHandler or questUI is null. Cannot update quest display.");
        }
    }
}
