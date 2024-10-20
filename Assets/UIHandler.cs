using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public DialogueUIHandler dialogueUI;
    public PromptUIHandler promptUI;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.UIHandler = this;
    }
}
