using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            gameObject.AddComponent<CanvasGroup>();
        }
    }

    protected virtual void HideUI(bool state = true)
    {
        canvasGroup.alpha = state ? 0 : 1;
    }
}
