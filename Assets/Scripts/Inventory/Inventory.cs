using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    public static Inventory instance { get; private set; }

    [SerializeField]
    List<InventoryItem> inventory = new List<InventoryItem>();

    [SerializeField] InventoryItem inventoryItem;
    [SerializeField] Transform itemsInInventory;
    [SerializeField] Inventory_UI ui;

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

    public InventoryItem[] GetAllItems()
    {
        return inventory.ToArray();
    }

    public void AddItem(Item newItem)
    {
        bool foundItem = false;

        //Check if item already exists within inventory and is stackable.
        if (newItem.isStackable && inventory.Count > 0)
        {
            foreach (InventoryItem existingItem in inventory)
            {
                if(existingItem.item == newItem)
                {
                    foundItem = true;
                    existingItem.count++;
                    Debug.Log(existingItem.count);
                    break;
                }
            }
        }

        if(!foundItem)
        {
            inventory.Add(CreateNewInventoryItem(newItem));
        }

        Debug.Log("Added " + newItem + " to inventory.");
        ui.GenerateInventorySlots();
    }

    InventoryItem CreateNewInventoryItem(Item newItem)
    {
        InventoryItem newInventoryItem = Instantiate(inventoryItem, transform.position, Quaternion.identity, itemsInInventory);
        newInventoryItem.item = newItem;
        return newInventoryItem;
    }

    public void RemoveItem()
    {

    }
}
