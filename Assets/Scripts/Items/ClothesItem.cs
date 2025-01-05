using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Clothes Item", menuName = "Item/Equipable/Clothing")]
public class ClothesItem : EquipableItem
{
    public float temperatureNegation;
    public override void EquipItem()
    {
        PlayerEquipment.instance.SetClothing(this);
        UI_Manager.instance.UpdateEquippedItems();
    }
}
