using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controls : MonoBehaviour
{
    [Header("Other Player Scripts")]
    [SerializeField] Player_Movement pm;
    [SerializeField] Player_Mouse_Look pml;

    [Header("Controls")]
    [SerializeField] PlayerControls playerControls;
    public InputAction move;
    public InputAction jump;
    public InputAction mouseX;
    public InputAction mouseY;

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
    }
    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        mouseX.Disable();
        mouseY.Disable();
    }
    #endregion

    private void Awake()
    {
        pm = GetComponent<Player_Movement>();
        pml = GetComponent<Player_Mouse_Look>();
        playerControls = new PlayerControls();
    }
}
