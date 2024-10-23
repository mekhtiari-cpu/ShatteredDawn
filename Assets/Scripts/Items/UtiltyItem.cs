using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Utility Item", menuName = "Item/Equipable/Utility")]
public class Utility : EquipableItem
{
    public override void EquipItem()
    {
        Debug.Log("Now holding " + itemName);
    }
}
