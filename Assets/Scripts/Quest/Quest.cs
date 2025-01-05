using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Quest Data", menuName = "Quest System/Create New Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    public string description;
    
    public string startQuestDialogue;
    public string midQuestDialogue;
    public string completionDialogue;

    public Item rewardItemOnAccept;
    public Item rewardItemOnComplete;

    public Quest[] prerequisiteQuests;

    public bool isConfirmationRequired = true;
    public bool isCompleted = false;
    public bool turnedIn = false;

    public QuestCompletionCondition[] completionConditions;
    public bool ArePrerequisitesMet()
    {
        if(prerequisiteQuests.Length == 0)
        {
            return true;
        }

        foreach (Quest prerequisite in prerequisiteQuests)
        {
            if (prerequisite == null)
            {
                Debug.LogWarning($"A quest prerequisite is null in {questName}.");
                continue;
            }

            if (!prerequisite.isCompleted)
            {
                return false;
            }
        }
        return true;
    }

    public bool AreCompletionConditionsMet(PlayerQuestHandler questHandler)
    {
        if (completionConditions.Length == 0)
        {
            return true;
        }

        foreach (QuestCompletionCondition condition in completionConditions)
        {
            if (condition == null)
            {
                Debug.LogWarning($"A quest completion condition is null in {questName}.");
                continue;
            }

            if (!condition.IsConditionMet(questHandler))
            {
                return false;
            }
        }
        return true;
    }
}
