using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] Item itemObj;
    public Quest quest;

    public override void Interact()
    {
        base.Interact();
        Inventory.instance.AddItem(itemObj);

        PlayerQuestHandler questHandler = GameManager.instance.playerQuest;
        if(questHandler != null)
        {
            if (quest != null)
            {
                questHandler.AddQuest(quest);
            }

            foreach (Quest quest in questHandler.activeQuests)
            {
                if (quest.isCompleted || quest.turnedIn)
                {
                    continue;
                }

                foreach (QuestCompletionCondition condition in quest.completionConditions)
                {
                    if (condition.completionType == QuestCompletionType.GiveItems || condition.completionType == QuestCompletionType.CollectItems || condition.completionType == QuestCompletionType.PickUpItem)
                    {
                        condition.RegisterItemPickup(itemObj);
                       
                        questHandler.UpdateQuestDisplay();
                        questHandler.CheckQuestCompletionConditions();
                    }
                }
            }
            
        }
        

        Destroy(this.gameObject);
    }
}
