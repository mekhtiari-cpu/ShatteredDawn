using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] TMP_Text itemNameText;
    [SerializeField] TMP_Text countText;

    public Item item;
    public string itemName;
    public int count;

    private void Awake()
    {
        itemNameText.text = itemName;
        countText.text = count.ToString();
    }

    public void UseItem()
    {
        Debug.Log("Using " + item.itemName);
        UI_Manager.instance.SelectItem(item);
    }
}
