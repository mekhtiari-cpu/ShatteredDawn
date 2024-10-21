using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfirmationUIHandler : UIHandler
{
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI descriptionText;
    public Button yesButton;
    public Button noButton;

    private Quest currentQuest; 
    private BaseNPC currentNPC;         
    private PlayerQuestHandler currentQuestHandler;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup)
        {
            ClosePanel();
        }
        
    }
    public void ShowQuestConfirmation(Quest quest, BaseNPC npc, PlayerQuestHandler questHandler)
    {
        // Set the current quest and NPC
        currentQuest = quest;
        currentNPC = npc;
        currentQuestHandler = questHandler;

        // Update the UI with quest details
        string header = quest.questName;
        string description = $"{quest.description}\n\nDo you accept?";

        ShowConfirmation(header, description, "Yes", "No");

        // Set up listeners for yes and no buttons
        yesButton.onClick.AddListener(OnAcceptQuest);
        noButton.onClick.AddListener(OnDeclineQuest);
    }

    // General confirmation function that accepts custom parameters for the UI
    public void ShowConfirmation(string header, string description, string yesText, string noText)
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        headerText.text = header;
        descriptionText.text = description;

        yesButton.GetComponentInChildren<TextMeshProUGUI>().text = yesText;
        noButton.GetComponentInChildren<TextMeshProUGUI>().text = noText;

        HideUI(false);
    }

    private void OnAcceptQuest()
    {
        // Notify NPC that the player accepted the quest
        currentNPC.OnQuestAccepted(currentQuestHandler);

        // Close the confirmation panel
        ClosePanel();
    }

    private void OnDeclineQuest()
    {
        // Notify NPC that the player declined the quest
        currentNPC.OnQuestDeclined();

        // Close the confirmation panel
        ClosePanel();
    }

    // Helper method to close the confirmation panel and remove listeners
    private void ClosePanel()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        // Close the confirmation panel
        HideUI();

        // Remove listeners to prevent duplicates
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
    }

    protected override void HideUI(bool state = true)
    {
        base.HideUI(state);

        UIHandlerManager uihandler = GameManager.instance.UIHandler;
        if(uihandler)
        {
            uihandler.cursor.SetActive(state);
            uihandler.promptUI.gameObject.SetActive(state);
        }
    }
}
