using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Item/Consumable/Health")]
public class HealthItem : ConsumableItem
{
    public int healthGainAmount;

    public override void ConsumeItem()
    {
        bool hasGainedHealth = PlayerEquipment.instance.gameObject.GetComponent<HealthManager>().GainHealth(healthGainAmount);
        if(hasGainedHealth)
        {
            Inventory.instance.RemoveItem(this);
        }
    }
}
