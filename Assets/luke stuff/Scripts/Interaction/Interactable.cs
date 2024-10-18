using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected string objectName;
    [SerializeField] protected LayerMask playerLayer;

    public virtual void Interact()
    {
        Debug.Log("Interacting with player");
    }
}
