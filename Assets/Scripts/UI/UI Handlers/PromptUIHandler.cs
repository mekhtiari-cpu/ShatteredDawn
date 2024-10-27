using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Could possibly make a base class and have all UIHandler inherit from there as there is many simialr functions
public class PromptUIHandler : UIHandler
{
    public TextMeshProUGUI displayText;
    // Start is called before the first frame update
    void Start()
    {
        if (!displayText)
        {
            displayText = GetComponentInChildren<TextMeshProUGUI>();
        }

        if (displayText == null)
        {
            Debug.LogWarning("PromptUIHandler: No TextMeshProUGUI component found for display text.");
        }

        canvasGroup = GetComponent<CanvasGroup>();
        HideDisplay();
    }

    public void SetDisplayText(string str)
    {
        displayText.text = str;
        ShowDisplay();
    }

    public void HideDisplay()
    {
        HideUI();
    }

    public void ShowDisplay()
    {
        HideUI(false);
    }
}
