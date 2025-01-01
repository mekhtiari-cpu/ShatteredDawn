using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    InventoryItem[] inventoryItems;

    [SerializeField] ItemSlot itemSlotTemplate;
    [SerializeField] GameObject inventorySlotsParent;

    public void GenerateInventorySlots()
    {
        ClearSlots();
        inventoryItems = Inventory.instance.GetAllItems();
        foreach (InventoryItem i in inventoryItems)
        {
            ItemSlot newItemSlot = itemSlotTemplate;
            newItemSlot.item = i.item;
            newItemSlot.itemName = i.item.itemName;
            newItemSlot.count = i.count;
            Instantiate(newItemSlot, transform.position, Quaternion.identity, inventorySlotsParent.transform);
        }
    }
    void ClearSlots()
    {
        foreach (Transform oldInventorySlot in inventorySlotsParent.transform)
        {
            Destroy(oldInventorySlot.gameObject);
        }
    }
}
