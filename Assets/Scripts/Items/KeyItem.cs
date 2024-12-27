using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Key Item", menuName = "Item/Key Item")]
public class KeyItem : Item
{
    public override void UseItem()
    {
        Debug.Log("Using " + itemName);
    }
}
