using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected string objectName;

    public virtual void Interact()
    {
        //Debug.Log("Interacting with player");
    }

    public virtual void StopInteracting()
    {
        Debug.Log("No longer interacting with player");
    }
}
