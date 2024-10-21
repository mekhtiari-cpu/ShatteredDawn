using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controls : MonoBehaviour
{
    [Header("Other Script References")]
    [SerializeField] Player_Movement pm;
    [SerializeField] UI_Manager ui_Manager;

    [Header("Controls")]
    [SerializeField] PlayerControls playerControls;
    public InputAction move;
    public InputAction jump;
    public InputAction mouseX;
    public InputAction mouseY;
    public InputAction inventory;

    #region toggle player controls
    private void OnEnable()
    {
        move = playerControls.Player.Move;
        playerControls.Enable();

        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += pm.Jump;

        mouseX = playerControls.Player.MouseX;
        mouseX.Enable();

        mouseY = playerControls.Player.MouseY;
        mouseY.Enable();

        inventory = playerControls.Player.Inventory;
        inventory.performed += ui_Manager.ToggleInventory;
        inventory.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        mouseX.Disable();
        mouseY.Disable();
        inventory.Disable();
    }
    #endregion

    private void Awake()
    {
        pm = GetComponent<Player_Movement>();
        ui_Manager = FindFirstObjectByType<UI_Manager>();
        playerControls = new PlayerControls();
    }

#if UNITY_EDITOR
    void OnDebug()
    {
        if(DebugManager.instance)
        {
            if (value.isPressed)
            {
                GameManager.instance.inDebug = !GameManager.instance.inDebug;
                DebugManager.instance.gameObject.SetActive(GameManager.instance.inDebug);
                Cursor.lockState = GameManager.instance.inDebug ? CursorLockMode.None : CursorLockMode.Locked;
            }
        }
    }
#endif
}
