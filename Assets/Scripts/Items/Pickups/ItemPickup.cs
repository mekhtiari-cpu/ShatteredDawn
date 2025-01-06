using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] Item itemObj;
    public Quest questToGive;
    public Quest relatedQuest;

    public GameObject poi;
    private void Start()
    {
        DisplayPOI();
    }

    public void DisplayPOI()
    {
        PlayerQuestHandler questHandler = GameManager.instance.playerQuest;
        if (!questHandler.activeQuests.Contains(relatedQuest))
        {
            if (poi)
            {
                poi.SetActive(false);
            }
        }
        else
        {
            if(poi)
            { 
                poi.SetActive(true); 
            }
        }
    }
    public override void Interact()
    {
        base.Interact();
        PlayerQuestHandler questHandler = GameManager.instance.playerQuest;
        
        if (questHandler != null)
        {
            if (relatedQuest)
            {
                if (!questHandler.activeQuests.Contains(relatedQuest))
                {
                    return;
                }
            }

            if (questToGive != null)
            {
                questHandler.AddQuest(questToGive);
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
        Inventory.instance.AddItem(itemObj);


        Destroy(this.gameObject);
    }
}
