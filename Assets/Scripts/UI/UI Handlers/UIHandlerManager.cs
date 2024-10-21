using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandlerManager : MonoBehaviour
{
    public DialogueUIHandler dialogueUI;
    public PromptUIHandler promptUI;
    public ConfirmationUIHandler confirmUI;
    public QuestUIHandler questUI;

    public GameObject cursor;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.UIHandler = this;
    }
}
