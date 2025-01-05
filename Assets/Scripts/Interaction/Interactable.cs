using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected string objectName;

    public virtual void Interact() {}

    public virtual void StopInteracting() { }
}
