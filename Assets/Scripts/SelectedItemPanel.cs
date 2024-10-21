using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectedItemPanel : MonoBehaviour
{
    public static SelectedItemPanel instance { get; private set; }

    [SerializeField] TMP_Text selectedItemName;
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
    }
}
