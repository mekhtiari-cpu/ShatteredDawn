using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUIHandler : UIHandler
{
    private BaseNPC speaker;
    public TextMeshProUGUI dialogueText;

    [SerializeField] private float dialogueDisplayTime = 3f;
    private Coroutine hideDialogueCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        if (!dialogueText)
        {
            dialogueText = GetComponentInChildren<TextMeshProUGUI>();
        }

        canvasGroup = GetComponent<CanvasGroup>();
        HideDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDialogueText(string dialogue, BaseNPC nPC = null)
    {
        if (hideDialogueCoroutine != null)
        {
            StopCoroutine(hideDialogueCoroutine);
        }
        HideUI(false);

        speaker = nPC;
        string prefix = speaker != null ? $"{speaker.npcName}: ": string.Empty;
        dialogueText.text = prefix + dialogue;

        hideDialogueCoroutine = StartCoroutine(HideAfterTime());
    }

    public void HideDialogue()
    {
        HideUI();
    }

    private IEnumerator HideAfterTime()
    {
        yield return new WaitForSeconds(dialogueDisplayTime);
        HideDialogue();
    }
}
