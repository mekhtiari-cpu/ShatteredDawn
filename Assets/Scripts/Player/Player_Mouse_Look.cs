using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Mouse_Look : MonoBehaviour
{
    [SerializeField] float sensX, sensY;
    [SerializeField] float mouseX, mouseY;
    [SerializeField] float x_rot_limit = 90f;

    float rotX = 0f;

    Player_Controls pc;

    [SerializeField] Transform playerTransform;
    [SerializeField] Transform cameraTransform;

    private INPC currentNPC;
    [SerializeField] private int interactionDistance = 10;
    private void Update()
    {
#if UNITY_EDITOR
        if (GameManager.instance.inDebug)
        {
            return;
        }
#endif

        CameraControls();
        RaycastForNPC();
    }

    void CameraControls()
    {
        mouseX = pc.mouseX.ReadValue<float>() * sensX * Time.deltaTime;
        mouseY = pc.mouseY.ReadValue<float>() * sensY * Time.deltaTime;

        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, -x_rot_limit, x_rot_limit);

        cameraTransform.localRotation = Quaternion.Euler(rotX, 0, 0);
        playerTransform.Rotate(Vector3.up * mouseX); 
    }

    private void Start()
    {
        pc = GetComponent<Player_Controls>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Perform a raycast to detect NPCs
    private void RaycastForNPC()
    {
        RaycastHit hit;
        // Cast a ray from the camera's position, pointing forward
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactionDistance))
        {
            INPC npc = hit.collider.GetComponent<INPC>();
            if (npc != null)
            {
                currentNPC = npc;
                // Display a prompt to the player
                Debug.Log("Press E to interact with " + npc.GetNPCType());
            }
            else
            {
                currentNPC = null;
            }
        }
        else
        {
            currentNPC = null;
        }
    }

}
