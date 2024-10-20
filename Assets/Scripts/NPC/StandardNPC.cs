using UnityEngine;

public class StandardNPC : BaseNPC, INPC
{
    [SerializeField] private string[] dialogueLines;
    private int currentLine = 0;
    private void Start()
    {
        // Initialize with some sample dialogue
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
    public void Interact()
    {
        if (dialogueLines.Length == 0)
        { 
            return; 
        }

        UIHandler uIHandler = GameManager.instance.UIHandler;
        DialogueUIHandler dialogueUI = uIHandler.dialogueUI;

        string displayText = dialogueLines[currentLine];
        dialogueUI.SetDialogueText(displayText, this);
        //Debug.Log(dialogueLines[currentLine]);

        currentLine = (currentLine + 1) % dialogueLines.Length;
    }

    public string GetNPCType()
    {
        return "StandardNPC";
    }
}
