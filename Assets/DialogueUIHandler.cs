using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUIHandler : MonoBehaviour
{
    private BaseNPC speaker;
    public TextMeshProUGUI dialogueText;

    CanvasGroup canvasGroup;

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
        canvasGroup.alpha = 1;

        speaker = nPC;
        string prefix = speaker != null ? $"{speaker.npcName}: ": string.Empty;
        dialogueText.text = prefix + dialogue;

        hideDialogueCoroutine = StartCoroutine(HideAfterTime());
    }

    public void HideDialogue()
    {
        canvasGroup.alpha = 0;
    }

    private IEnumerator HideAfterTime()
    {
        yield return new WaitForSeconds(dialogueDisplayTime);
        HideDialogue();
    }
}
