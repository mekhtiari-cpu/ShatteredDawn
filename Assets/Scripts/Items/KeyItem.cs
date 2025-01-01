using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Key Item", menuName = "Item/Key Item")]
public class KeyItem : Item
{
    public enum KeyItemType {Tire, Battery, OilCan, IgnitionKey }
    public KeyItemType keyItemType;

    public override void UseItem()
    {
        if(Player_Interact.instance.PlayerNearCar())
        {
            Debug.Log("Returning " + itemName);
            Player_Interact.instance.ReturnKeyItem(this);
        }
        else
        {
            Debug.Log("You must be near the car to use this key item.");
        }
    }
}
