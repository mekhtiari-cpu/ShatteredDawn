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
        if (GameManager.instance != null)
        {
            GameManager.instance.UIHandler = this;
        }

        if (cursor != null)
        {
            cursor.SetActive(Cursor.lockState == CursorLockMode.Locked); // Or any initial state you'd like
        }

    }
}
