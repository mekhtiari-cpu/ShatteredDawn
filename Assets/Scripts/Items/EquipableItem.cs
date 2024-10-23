using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipableItem : Item
{
    public override void UseItem()
    {
        EquipItem();
    }

    public abstract void EquipItem();
}
