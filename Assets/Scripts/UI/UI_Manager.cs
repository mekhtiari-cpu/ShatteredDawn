using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance { get; private set; }

    [SerializeField] Player_Controls pc;

    [SerializeField] GameObject inventory;
    [SerializeField] SelectedItemPanel selectedItemPanel;
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
            DontDestroyOnLoad(gameObject);
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
}
