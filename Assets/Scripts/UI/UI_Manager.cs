using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] Player_Controls pc;

    [SerializeField] GameObject inventory;

    public void ToggleInventory(InputAction.CallbackContext context)
    {
        inventory.SetActive(!inventory.activeSelf);
    }
}
