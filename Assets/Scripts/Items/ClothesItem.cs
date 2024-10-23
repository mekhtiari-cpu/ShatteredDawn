using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Clothes Item", menuName = "Item/Equipable/Clothing")]
public class ClothesItem : EquipableItem
{
    public float temperatureNegation;
    public override void EquipItem()
    {
        Debug.Log("Now wearing " + itemName + " and negating effects of temperature by " + temperatureNegation);
    }
}
