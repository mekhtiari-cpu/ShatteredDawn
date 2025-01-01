using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Item/Equipable/Weapon")]
public class WeaponItem : EquipableItem
{
    public int damage;
    public GameObject weaponPrefab;
    public override void EquipItem()
    {
        Debug.Log("Now wielding " + itemName);
        PlayerEquipment.instance.SetWeapon(this);
        UI_Manager.instance.UpdateEquippedItems();
    }
}
