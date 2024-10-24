using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquippedItemsUI : MonoBehaviour
{
    [SerializeField] TMP_Text equippedWeaponName;
    [SerializeField] TMP_Text equippedUtilityName;
    [SerializeField] TMP_Text equippedClothingName;

    public void SetEquipmentUI()
    {
        WeaponItem newWeapon = PlayerEquipment.instance.GetWeapon();
        UtilityItem newUtility = PlayerEquipment.instance.GetUtility();
        ClothesItem newClothing = PlayerEquipment.instance.GetClothing();

        if (newWeapon != null)
            equippedWeaponName.text = newWeapon.itemName;
        else
            equippedWeaponName.text = "None";

        if (newUtility != null)
            equippedUtilityName.text = newUtility.itemName;
        else
            equippedUtilityName.text = "None";

        if (newClothing != null)
            equippedClothingName.text = newClothing.itemName;
        else
            equippedClothingName.text = "None";
    }
}
