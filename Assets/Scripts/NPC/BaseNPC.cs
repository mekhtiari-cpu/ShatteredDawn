using System.Collections.Generic;
using UnityEngine;

public class BaseNPC : MonoBehaviour, INPC
{
    public string npcName;

    public List<Quest> allQuests = new List<Quest>();
    public Quest assignedQuest { get; private set; }

    public GameObject pointOfInterest;

    [SerializeField] protected bool questGiven = false;

    public virtual void Interact(PlayerQuestHandler questHandler) {}
    public virtual string GetNPCType() { return string.Empty; }
    public void OnQuestAccepted(PlayerQuestHandler questHandler)
    {
        questHandler.AddQuest(assignedQuest);
        questGiven = true;

        UIHandlerManager uIHandler = GameManager.instance.UIHandler;
        DialogueUIHandler dialogueUI = uIHandler.dialogueUI;

        string acceptanceText = assignedQuest.rewardItemOnAccept != null
          ? $"Thank you for accepting the task! Here's {assignedQuest.rewardItemOnAccept.itemName} to help you get started."
          : "Thank you for accepting the task!";

        dialogueUI.SetDialogueText(assignedQuest.startQuestDialogue == string.Empty ? acceptanceText : assignedQuest.startQuestDialogue, this);
    }

    public void OnQuestDeclined()
    {
        UIHandlerManager uIHandler = GameManager.instance.UIHandler;
        DialogueUIHandler dialogueUI = uIHandler.dialogueUI;

        dialogueUI.SetDialogueText("Maybe another time.", this);
    }

    // Find the first incomplete quest in the list
    public void UpdateAssignedQuest()
    {
        foreach (Quest quest in allQuests)
        {
            if (!quest.turnedIn && quest.ArePrerequisitesMet())
            {
                assignedQuest = quest;
                bool showInterest = assignedQuest.isCompleted;
                pointOfInterest.SetActive(showInterest);
                return;
            }
        }

       
        assignedQuest = null;
    }

    public void GiveReward(Quest quest)
    {
        // Logic for giving rewards (e.g., experience points, items, currency, etc.)
        Debug.Log($"{npcName} has given you a reward for completing {quest.questName}.");
    }
    public bool HasQuest()
    {
        return assignedQuest != null;
    }

    protected void ShowQuestInProgressDialogue()
    {
        UIHandlerManager uIHandler = GameManager.instance.UIHandler;
        DialogueUIHandler dialogueUI = uIHandler.dialogueUI;

        string progressText = string.IsNullOrEmpty(assignedQuest.midQuestDialogue)
            ? "Keep working on the quest!"
            : assignedQuest.midQuestDialogue;
        dialogueUI.SetDialogueText(progressText, this);
    }

    protected void ShowQuestCompletedDialogue()
    {
        UIHandlerManager uIHandler = GameManager.instance.UIHandler;
        DialogueUIHandler dialogueUI = uIHandler.dialogueUI;

        string completedText = string.IsNullOrEmpty(assignedQuest.completionDialogue)
           ? "Thanks for completing the quest!"
           : assignedQuest.completionDialogue;
        dialogueUI.SetDialogueText(completedText, this);
    }
    protected void ShowQuestTurnInDialogue()
    {
        UIHandlerManager uIHandler = GameManager.instance.UIHandler;
        DialogueUIHandler dialogueUI = uIHandler.dialogueUI;

        string turnInText = string.IsNullOrEmpty(assignedQuest.completionDialogue)
            ? "Thank you for completing the quest!"
            : assignedQuest.completionDialogue;
        dialogueUI.SetDialogueText(turnInText, this);
    }

    protected void CompleteQuestAndCheckForNext(PlayerQuestHandler questHandler)
    {
        assignedQuest.isCompleted = true;
        questHandler.CompleteQuest(assignedQuest);

        UpdateAssignedQuest();
        questGiven = false;

        UIHandlerManager uIHandler = GameManager.instance.UIHandler;
        DialogueUIHandler dialogueUI = uIHandler.dialogueUI;

        string nextQuestText = assignedQuest == null
            ? "Thanks for completing all my quests! Let's get back to business."
            : "Great work! Here's your next task.";
        dialogueUI.SetDialogueText(nextQuestText, this);
    }

    protected void PromptQuestConfirmation(PlayerQuestHandler questHandler)
    {
        UIHandlerManager uIHandler = GameManager.instance.UIHandler;
        ConfirmationUIHandler questConfirmationUI = uIHandler.confirmUI;

        questConfirmationUI.ShowQuestConfirmation(assignedQuest, this, questHandler);
    }

    protected void GiveQuest(PlayerQuestHandler questHandler)
    {
        UIHandlerManager uIHandler = GameManager.instance.UIHandler;
        DialogueUIHandler dialogueUI = uIHandler.dialogueUI;

        string questStartText = string.IsNullOrEmpty(assignedQuest.description)
            ? $"Here’s a quest: {assignedQuest.questName}"
            : assignedQuest.description;
        dialogueUI.SetDialogueText(questStartText, this);

        questHandler.AddQuest(assignedQuest);
        questGiven = true;
    }

    public virtual string GetName()
    {
        return npcName;
    }
}
