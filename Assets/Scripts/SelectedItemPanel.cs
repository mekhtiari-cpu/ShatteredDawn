using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectedItemPanel : MonoBehaviour
{
    public static SelectedItemPanel instance { get; private set; }

    [SerializeField] TMP_Text selectedItemName;
    [SerializeField] TMP_Text selectedItemDescription;
    Item selectedItem; 

    void Awake()
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

    public void UpdateUIForNewItem(Item item)
    {
        selectedItem = item;
        selectedItemName.text = selectedItem.itemName;
        selectedItemDescription.text = selectedItem.itemDescription;
    }

    public void UseItem()
    {
        selectedItem.UseItem();
    }

    public void RemoveItem()
    {
        bool itemStillExists = Inventory.instance.RemoveItem(selectedItem);
        if(!itemStillExists)
        {
            selectedItem = null;
            gameObject.SetActive(false);
        }
    }
}
