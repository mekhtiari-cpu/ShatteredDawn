using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectedItemPanel : MonoBehaviour
{
    [SerializeField] TMP_Text selectedItemName;
    [SerializeField] TMP_Text selectedItemDescription;
    Item selectedItem; 

    public void UpdateUIForNewItem(Item item)
    {
        selectedItem = item;
        selectedItemName.text = selectedItem.itemName;
        selectedItemDescription.text = selectedItem.itemDescription;
    }

    public void UseItem()
    {
        selectedItem.UseItem();
        DeselectItem();
    }

    public void RemoveItem()
    {
        bool itemStillExists = Inventory.instance.RemoveItem(selectedItem);
        if(!itemStillExists)
        {
            DeselectItem();
        }
    }

    void DeselectItem()
    {
        selectedItem = null;
        gameObject.SetActive(false);
    }
}
