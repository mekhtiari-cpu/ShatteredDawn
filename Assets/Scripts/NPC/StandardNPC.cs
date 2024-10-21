using UnityEngine;

public class StandardNPC : BaseNPC
{
    [SerializeField] private string[] dialogueLines;
    private int currentLine = 0;

    private void Start()
    {
        // Initialize default dialogue
        if (dialogueLines.Length == 0)
        {
            dialogueLines = new string[]
            {
                "Hello, traveler. Welcome.",
                "It's good to see you again.",
                "Take care on your journey."
            };
        }
    }

    public override void Interact(PlayerQuestHandler questHandler)
    {
        UpdateAssignedQuest();

        if (assignedQuest != null && !questGiven)
        {
            if (assignedQuest.isConfirmationRequired)
            {
                PromptQuestConfirmation(questHandler);
            }
            else
            {
                GiveQuest(questHandler);
            }
        }
        else if (assignedQuest != null && questGiven && !assignedQuest.isCompleted)
        {
            ShowQuestInProgressDialogue();
        }
        else if (assignedQuest != null && assignedQuest.isCompleted && !assignedQuest.turnedIn)
        {
            ShowQuestTurnInDialogue();
            questHandler.TurnInQuest(assignedQuest, this);
            questGiven = false;
        }
        else
        {
            ShowStandardDialogue();
        }
    }

    private void ShowStandardDialogue()
    {
        UIHandlerManager uIHandler = GameManager.instance.UIHandler;
        DialogueUIHandler dialogueUI = uIHandler.dialogueUI;

        string displayText = dialogueLines[currentLine];
        dialogueUI.SetDialogueText(displayText, this);

        currentLine = (currentLine + 1) % dialogueLines.Length;
    }

    public override string GetNPCType()
    {
        return "StandardNPC";
    }
}
