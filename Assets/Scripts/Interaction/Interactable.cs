using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected string objectName;

    public virtual void Interact()
    {
        Debug.Log(objectName + " is interacting with player");
    }

    public virtual void StopInteracting()
    {
        Debug.Log(objectName + " no longer interacting with player");
    }
}
