using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] Item itemObj;

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Player picked up " + objectName);
        Inventory.instance.AddItem(itemObj);
        Destroy(this.gameObject);
    }
}
