using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public static PlayerEquipment instance { get; private set; }

    [SerializeField] WeaponItem equippedWeapon;
    [SerializeField] UtilityItem equippedUtility;
    [SerializeField] ClothesItem equippedClothing;

    private void Awake()
    {
        //Singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetWeapon(WeaponItem newWeapon)
    {
        if (equippedWeapon != null)
            Inventory.instance.AddItem(equippedWeapon);
        equippedWeapon = newWeapon;
        Inventory.instance.RemoveItem(equippedWeapon);
    }
    public void SetUtility(UtilityItem newUtility)
    {
        equippedUtility = newUtility;
        Inventory.instance.RemoveItem(equippedUtility);
    }
    public void SetClothing(ClothesItem newClothing)
    {
        if (equippedClothing != null)
            Inventory.instance.AddItem(equippedClothing);
        equippedClothing = newClothing;
        float oldDecayRate = Player_Temperature_Manager.instance.GetDecayRate();
        Player_Temperature_Manager.instance.SetTempDecayRate(oldDecayRate * equippedClothing.temperatureNegation);
        Inventory.instance.RemoveItem(equippedClothing);
    }

    public WeaponItem GetWeapon()
    {
        return equippedWeapon;
    }
    public UtilityItem GetUtility()
    {
        return equippedUtility;
    }
    public ClothesItem GetClothing()
    {
        return equippedClothing;
    }

    public void RemoveWeapon()
    {
        Inventory.instance.AddItem(equippedWeapon);
        equippedWeapon = null;
        UI_Manager.instance.UpdateEquippedItems();
    }

    public void RemoveUtility()
    {
        equippedUtility = null;
        UI_Manager.instance.UpdateEquippedItems();
    }

    public void RemoveClothing()
    {
        Inventory.instance.AddItem(equippedClothing);
        equippedClothing = null;
        Player_Temperature_Manager.instance.ResetDecayRate();
        UI_Manager.instance.UpdateEquippedItems();
    }
}
