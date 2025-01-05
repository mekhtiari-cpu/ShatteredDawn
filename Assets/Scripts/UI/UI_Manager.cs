using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance { get; private set; }

    [SerializeField] GameObject inventory;
    [SerializeField] SelectedItemPanel selectedItemPanel;
    [SerializeField] EquippedItemsUI equippedItemsUI;
    [SerializeField] InfoUI infoUI;
    [SerializeField] CarUI carUI;

    Item selectedItem;

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
        }
    }

    public void ToggleInventory(InputAction.CallbackContext context)
    {
        inventory.SetActive(!inventory.activeSelf);

        if (inventory.activeSelf)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    public bool GetInventoryState()
    {
        return inventory.activeSelf;
    }

    public void SelectItem(Item item)
    {
        if (selectedItemPanel.gameObject.activeSelf == true && item == selectedItem)
        {
            selectedItemPanel.gameObject.SetActive(false);
        }
        else
        {
            selectedItemPanel.gameObject.SetActive(true);
            selectedItemPanel.UpdateUIForNewItem(item);
            selectedItem = item;
        }
    }

    public void UpdateEquippedItems()
    {
        equippedItemsUI.SetEquipmentUI();
    }

    public void ReadInfo()
    {
        inventory.SetActive(false);
        infoUI.gameObject.SetActive(true);
        infoUI.DisplayInfo(selectedItem);
    }

    public CarUI GetCarUI()
    {
        return carUI;
    }
}
