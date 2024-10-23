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
        InventoryItem existingItem = FindItem(newItem);

        //Check if item already exists within inventory and is stackable.
        if (existingItem == null || !existingItem.item.isStackable)
        {
            inventory.Add(CreateNewInventoryItem(newItem));
        }
        else
        {
            existingItem.count++;
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

    //Search for a specific item in the inventory.
    public InventoryItem FindItem(Item itemToFind)
    {
        foreach (InventoryItem existingItem in inventory)
        {
            if (existingItem.item == itemToFind)
            {
                return existingItem;
            }
        }
        return null;
    }

    //Remove an item from the inventory
    public bool RemoveItem(Item itemToRemove)
    {
        InventoryItem existingItem = FindItem(itemToRemove);

        if(existingItem == null)
        {
            return false;
        }

        existingItem.count--;

        if(existingItem.count <= 0)
        {
            inventory.Remove(existingItem);
            ui.GenerateInventorySlots();
            return false;
        }

        ui.GenerateInventorySlots();
        return true;
    }
}
