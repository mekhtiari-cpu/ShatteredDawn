using UnityEngine;

public enum QuestCompletionType
{
    None,
    SurviveDays,
    CollectItems,
    GiveItems
}

[System.Serializable]
public class QuestCompletionCondition
{
    public QuestCompletionType completionType;
    public int requiredAmount;

    public bool IsConditionMet(PlayerQuestHandler questHandler)
    {
        switch (completionType)
        {
            case QuestCompletionType.SurviveDays:
                return GameManager.instance.dayNightScript.GetCurrentDay() >= requiredAmount;

            /*case QuestCompletionType.CollectItems:
                return questHandler.HasItem(item, requiredAmount);

            case QuestCompletionType.GiveItems:
                return questHandler.GetItemCount(item) >= requiredAmount;*/

            default:
                return false;
        }
    }
}
