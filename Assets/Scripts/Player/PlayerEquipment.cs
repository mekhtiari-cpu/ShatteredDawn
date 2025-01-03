using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public static PlayerEquipment instance { get; private set; }

    [Header("Equipped Items")]
    [SerializeField] Gun equippedWeapon;
    [SerializeField] UtilityItem equippedUtility;
    [SerializeField] ClothesItem equippedClothing;

    [Header("Gameobject containers for equipped items")]
    [SerializeField] GameObject utilityHolder;

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

    public void SetWeapon(Gun newWeapon)
    {
        if (equippedWeapon != null)
            Inventory.instance.AddItem(equippedWeapon);
        equippedWeapon = newWeapon;
        Inventory.instance.RemoveItem(equippedWeapon);
        Weapon weaponScript = GetComponent<Weapon>();
        weaponScript.loadOut = newWeapon;
        weaponScript.Equip();
    }
    public void SetUtility(UtilityItem newUtility)
    {
        equippedUtility = newUtility;
        Instantiate(newUtility.utilityPrefab, utilityHolder.transform.position, Quaternion.identity, utilityHolder.transform);
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

    public Gun GetWeapon()
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
        Weapon weaponScript = GetComponent<Weapon>();
        weaponScript.DropWeapon();
    }

    public void RemoveUtility()
    {
        equippedUtility = null;
        Destroy(utilityHolder.transform.GetChild(0).gameObject);
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
