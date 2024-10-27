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
        else
        {
            Debug.LogWarning("GameManager instance is null. UIHandlerManager could not initialize.");
        }

        if (dialogueUI == null) Debug.LogWarning("Dialogue UI Handler is not assigned.");
        if (promptUI == null) Debug.LogWarning("Prompt UI Handler is not assigned.");
        if (confirmUI == null) Debug.LogWarning("Confirmation UI Handler is not assigned.");
        if (questUI == null) Debug.LogWarning("Quest UI Handler is not assigned.");

        if (cursor == null)
        {
            Debug.LogWarning("Cursor GameObject is not assigned.");
        }
        else
        {
            cursor.SetActive(Cursor.lockState == CursorLockMode.Locked); // Or any initial state you'd like
        }

    }
}
