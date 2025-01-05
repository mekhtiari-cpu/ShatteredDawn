using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputControls : MonoBehaviour
{
    [Header("Other Script References")]
    private Player_Movement pm;
    private UI_Manager ui_Manager;
    private Weapon weapon;
    private Player_Interact pi;

    [Header("Controls")]
    [SerializeField] PlayerControls playerControls;
    public InputAction move;
    public InputAction jump;
    public InputAction mouseX;
    public InputAction mouseY;
    public InputAction inventory;
    public InputAction crouch;
    public InputAction interact;

    #region toggle player controls
    private void OnEnable()
    {
        OnDisable(); // clear any existing for safety

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

        crouch = playerControls.Player.Crouch;
        crouch.Enable();
        crouch.performed += pm.Crouch;

        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += pi.ToggleCarStatusUI;
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

        pi = GetComponent<Player_Interact>();
    }
}
