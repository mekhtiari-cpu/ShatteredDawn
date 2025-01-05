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
            Player_Interact.instance.ReturnKeyItem(this);
            Inventory.instance.RemoveItem(this);
        }
    }
}
