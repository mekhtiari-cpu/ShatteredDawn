using System.Collections.Generic;
using UnityEngine;

public class BaseNPC : MonoBehaviour, INPC
{
    public string npcName;

    public List<Quest> allQuests = new List<Quest>();
    public Quest assignedQuest { get; private set; }

    [SerializeField] protected bool questGiven = false;

    public virtual void Interact(PlayerQuestHandler questHandler) {}
    public virtual string GetNPCType() { return string.Empty; }
    public void OnQuestAccepted(PlayerQuestHandler questHandler)
    {
        questHandler.AddQuest(assignedQuest);
        questGiven = true;

        UIHandlerManager uIHandler = GameManager.instance.UIHandler;
        DialogueUIHandler dialogueUI = uIHandler.dialogueUI;

        dialogueUI.SetDialogueText("Thank you for accepting the task!", this);
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

        dialogueUI.SetDialogueText("Keep working on the quest!", this);
    }

    protected void ShowQuestCompletedDialogue()
    {
        UIHandlerManager uIHandler = GameManager.instance.UIHandler;
        DialogueUIHandler dialogueUI = uIHandler.dialogueUI;

        dialogueUI.SetDialogueText("Thanks for completing the quest!", this);
    }
    protected void ShowQuestTurnInDialogue()
    {
        UIHandlerManager uIHandler = GameManager.instance.UIHandler;
        DialogueUIHandler dialogueUI = uIHandler.dialogueUI;

        string turnInText = "Thank you for completing the quest!";
        dialogueUI.SetDialogueText(turnInText, this);

        Debug.Log($"Quest turned in: {assignedQuest.questName}");
    }

    protected void CompleteQuestAndCheckForNext(PlayerQuestHandler questHandler)
    {
        assignedQuest.isCompleted = true;
        questHandler.CompleteQuest(assignedQuest);

        UpdateAssignedQuest();
        questGiven = false;

        UIHandlerManager uIHandler = GameManager.instance.UIHandler;
        DialogueUIHandler dialogueUI = uIHandler.dialogueUI;

        dialogueUI.SetDialogueText("Thanks for completing all my quests! Let's get back to business.", this);
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

        dialogueUI.SetDialogueText($"Here’s a quest: {assignedQuest.questName}", this);

        questHandler.AddQuest(assignedQuest);
        questGiven = true;
    }


}
