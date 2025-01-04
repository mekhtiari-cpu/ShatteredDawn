using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumableItem : Item
{
    public override void UseItem()
    {
        ConsumeItem();
    }

    public abstract void ConsumeItem();
}
