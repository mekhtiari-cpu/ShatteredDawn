using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUIHandler : UIHandler
{
    public Transform parentTransform;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        HideUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDisplay(ref List<Quest> allQuests)
    {
        foreach (Transform child in parentTransform)
        {
            Destroy(child.gameObject);
        }

        if(allQuests.Count > 0) 
        {
            foreach (Quest item in allQuests)
            {
                GameObject newObj = Instantiate(prefab, parentTransform);
                TextMeshProUGUI textMesh = newObj.GetComponent<TextMeshProUGUI>();

                string completeSuffix = item.isCompleted ? "(Completed)" : "";
                textMesh.text = $"{item.questName + completeSuffix}";
                
                if(item.isCompleted || item.turnedIn)
                {
                    continue;
                }

                foreach (QuestCompletionCondition condition in item.completionConditions)
                {
                    if (condition.completionType == QuestCompletionType.GiveItems || condition.completionType == QuestCompletionType.CollectItems || condition.completionType == QuestCompletionType.PickUpItem)
                    {
                        if (condition.requiredAmount > 0)
                        {
                            string progressSuffix = $"({condition.requiredItem.name}: {condition.GetItemPickupCount()} / {condition.requiredAmount})";
                            textMesh.text = textMesh.text + progressSuffix;
                        }
                    }

                    if (condition.completionType == QuestCompletionType.KillEnemies)
                    {
                        if (condition.requiredAmount > 0)
                        {
                            string progressSuffix = $"({condition.enemyType} Killed: {condition.GetKillCount()} / {condition.requiredAmount})";
                            textMesh.text = textMesh.text + progressSuffix;
                        }
                    }

                    if (condition.GetInteractionCount() > 0)
                    {
                        if (condition.requiredAmount > 0)
                        {
                            string progressSuffix = $"({condition.targetNPC}: {condition.GetInteractionCount()} / {condition.requiredAmount})";
                            textMesh.text = textMesh.text + progressSuffix;
                        }
                    }
                }
            }
            HideUI(false);

        }
        else
        {
            HideUI();
        }
    }
}
