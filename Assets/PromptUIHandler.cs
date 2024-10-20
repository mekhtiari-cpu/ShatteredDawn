using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Could possibly make a base class and have all UIHandler inherit from there as there is many simialr functions
public class PromptUIHandler : MonoBehaviour
{
    public TextMeshProUGUI displayText;

    CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        if (!displayText)
        {
            displayText = GetComponentInChildren<TextMeshProUGUI>();
        }

        canvasGroup = GetComponent<CanvasGroup>();
        HideDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDisplayText(string str)
    {
        displayText.text = str;
        ShowDisplay();
    }

    public void HideDisplay()
    {
        canvasGroup.alpha = 0;
    }

    public void ShowDisplay()
    {
        canvasGroup.alpha = 1;
    }
}
