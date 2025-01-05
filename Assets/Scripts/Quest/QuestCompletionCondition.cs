using UnityEngine;

public enum QuestCompletionType
{
    None,
    SurviveDays,
    CollectItems,
    GiveItems,
    KillEnemies,
    InteractWithNPC,
    PickUpItem
}

[System.Serializable]
public class QuestCompletionCondition
{
    public QuestCompletionType completionType;
    public int requiredAmount;
    public Item requiredItem;
    public string targetNPC;
    public string enemyType;

    private int interactionCount = 0;
    private int itemPickupCount = 0;
    private int killCount = 0;

    public void Reset()
    {
        interactionCount = 0;
        itemPickupCount = 0;
        killCount = 0;
    }
    public bool IsConditionMet(PlayerQuestHandler questHandler)
    {
        switch (completionType)
        {
            case QuestCompletionType.SurviveDays:
                return GameManager.instance.dayNightScript.GetCurrentDay() >= requiredAmount;
            case QuestCompletionType.CollectItems:
                return questHandler.HasItem(requiredItem, requiredAmount);
            case QuestCompletionType.GiveItems:
                return questHandler.HasItem(requiredItem, requiredAmount);
            case QuestCompletionType.KillEnemies:
                return killCount >= requiredAmount;
            case QuestCompletionType.InteractWithNPC:
                return interactionCount >= requiredAmount;
            case QuestCompletionType.PickUpItem:
                return itemPickupCount >= requiredAmount;

            default:
                return false;
        }
    }

    public void RegisterNPCInteraction(BaseNPC npc)
    {
        if (completionType == QuestCompletionType.InteractWithNPC && npc.name == targetNPC)
        {
            interactionCount++;
            Debug.Log($"Interacted with NPC: {npc.npcName}. Count: {interactionCount}/{requiredAmount}");
        }
    }

    public void RegisterItemPickup(Item pickedUpItem)
    {
        if (pickedUpItem == requiredItem)
        {
            itemPickupCount++;
            Debug.Log($"Picked up item: {pickedUpItem.itemName}. Count: {itemPickupCount}/{requiredAmount}");
        }
    }

    public void RegisterEnemyKilled(int enemiesKilled)
    {
        killCount = enemiesKilled;
    }

    public int GetKillCount()
    {
        return killCount;
    }

    public int GetItemPickupCount()
    {
        return itemPickupCount;
    }

    public int GetInteractionCount()
    {
        return interactionCount;
    }
}
