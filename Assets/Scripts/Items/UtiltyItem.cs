using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Utility Item", menuName = "Item/Equipable/Utility")]
public class UtilityItem : EquipableItem
{
    public GameObject utilityPrefab;
    public override void EquipItem()
    {
        PlayerEquipment.instance.SetUtility(this);
        UI_Manager.instance.UpdateEquippedItems();
    }
}
