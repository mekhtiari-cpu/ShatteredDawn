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

                string suffix = item.isCompleted ? "(Completed)" : "";
                textMesh.text = $"{item.questName + suffix}";
            }
            HideUI(false);

        }
        else
        {
            HideUI();
        }
    }
}
