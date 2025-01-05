using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Mouse_Look : MonoBehaviour
{
    GameManager gm;
    PromptUIHandler promptUI;
    PlayerController playerController;

    [SerializeField] float sensX, sensY;
    [SerializeField] float mouseX, mouseY;
    [SerializeField] float x_rot_limit = 90f;

    float rotX = 0f;

    PlayerInputControls pc;

    [SerializeField] Transform playerTransform;

    [Header("FPS Camera")]
    [SerializeField] private Camera myCamera;
    [SerializeField] private Camera weaponCamera;
    [SerializeField] private Vector3 myCameraOrigin;
    private float baseFOV = 60f, sprintFOVModifier = 1.6f;
    public float crouchAmount;
    public float maxAngle;
    private Quaternion camCentre;

    private Vector3 normalCamTarget;
    private Vector3 weaponCamTarget;

    [Header("FPS Weapon")]
    public Transform weapon;
    private Vector3 weaponParentPosition;
    public Vector3 weaponOrigin;
    private Vector3 targetWeaponBob;

    private float movementCounter;
    private float idleCounter;

    private INPC currentNPC;
    [SerializeField] private int interactionDistance = 10;

    private void Start()
    {
        gm = GameManager.instance;
        if (gm)
        {
            if (gm.UIHandler)
            {
                promptUI = gm.UIHandler.promptUI;
            }
        }

        playerController = GetComponent<PlayerController>();
        pc = playerController.GetPlayerInputControlsScript();

        Cursor.lockState = CursorLockMode.Locked;

        myCamera = transform.Find("Cameras/Main Camera").GetComponent<Camera>();
        weaponCamera = transform.Find("Cameras/WeaponCamera").GetComponent<Camera>();

        if (myCamera)
        {
            camCentre = myCamera.transform.localRotation;
            myCameraOrigin = myCamera.transform.localPosition;
            baseFOV = myCamera.fieldOfView;
        }

        weapon = transform.Find("Weapons");
        if (weapon)
        {
            weaponOrigin = weapon.localPosition;
            weaponParentPosition = weaponOrigin;
        }

        normalCamTarget = myCamera.transform.localPosition;
        weaponCamTarget = weaponCamera.transform.localPosition;
    }
    private void Update()
    {
        weaponCamera.transform.rotation = myCamera.transform.rotation;

        CameraControls();
        WeaponCameraControls();
        RaycastForNPC();
    }
    void LateUpdate()
    {
        myCamera.transform.localPosition = normalCamTarget;
        weaponCamera.transform.localPosition = weaponCamTarget;
    }

    void FixedUpdate()
    {
        if (UI_Manager.instance.GetInventoryState())
        {
            return;
        }

        float targetFOV = playerController.isAiming
            ? baseFOV * 0.7f // Adjust to the aiming FOV
            : baseFOV;       // Default FOV

        // Smoothly interpolate to the target FOV
        myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, targetFOV, Time.deltaTime * 8f);
        weaponCamera.fieldOfView = Mathf.Lerp(weaponCamera.fieldOfView, targetFOV, Time.deltaTime * 8f);

        // Handle other camera adjustments
        weaponParentPosition = Vector3.Lerp(weaponParentPosition, weaponOrigin + Vector3.down, Time.deltaTime * 10f);
        normalCamTarget = Vector3.Lerp(normalCamTarget, myCameraOrigin + Vector3.down * crouchAmount, Time.deltaTime * 10f);
        weaponCamTarget = Vector3.Lerp(weaponCamTarget, myCameraOrigin + Vector3.down * crouchAmount, Time.deltaTime * 10f);
    }
    void CameraControls()
    {
        // null check
        if (pc != null)
        {
            mouseX = pc.mouseX.ReadValue<float>() * sensX * Time.deltaTime;
            mouseY = pc.mouseY.ReadValue<float>() * sensY * Time.deltaTime;
        }

        if (!UI_Manager.instance.GetInventoryState())
        {
            // ensure float precision is finite for webGl
            if (float.IsFinite(mouseX) && float.IsFinite(mouseY))
            {
                rotX -= mouseY;
                rotX = Mathf.Clamp(rotX, -x_rot_limit, x_rot_limit);
                myCamera.transform.localRotation = Quaternion.Euler(rotX, 0, 0);
                playerTransform.Rotate(Vector3.up * mouseX);
                weapon.rotation = myCamera.transform.rotation;
            }
        }
    }
    void WeaponCameraControls()
    {
        if (playerController.firingState == playerFiringState.HipFire)
        {
            Bob(movementCounter, 0.02f, 0.02f);
            movementCounter += Time.deltaTime * 5;
            weapon.localPosition = Vector3.MoveTowards(weapon.localPosition, targetWeaponBob, Time.deltaTime * 10f * 0.2f);

        }
        else if (playerController.firingState == playerFiringState.Ads)
        {
            Bob(movementCounter, 0.002f, 0.004f);
            movementCounter += Time.deltaTime * 5;
            weapon.localPosition = Vector3.MoveTowards(weapon.localPosition, targetWeaponBob, Time.deltaTime * 10f * 0.2f);
        }
    }
    void Bob(float tempZ, float tempYIntensisty, float tempXIntensity)
    {
        targetWeaponBob = weaponParentPosition + new Vector3(Mathf.Cos(tempZ) * tempXIntensity, Mathf.Sin(tempZ) * tempYIntensisty * 2, 0);
    }

    // Perform a raycast to detect NPCs
    private void RaycastForNPC()
    {
        if(!promptUI)
        {
            gm = GameManager.instance;
            if (gm)
            {
                if (gm.UIHandler)
                {
                    promptUI = gm.UIHandler.promptUI;    
                }
            }
        }

        if (float.IsFinite(interactionDistance) && interactionDistance > 0)
        {
            RaycastHit hit;
            // Cast a ray from the camera's position, pointing forward
            if (Physics.Raycast(myCamera.transform.position, myCamera.transform.forward, out hit, interactionDistance))
            {
                INPC npc = hit.collider.GetComponent<INPC>();
                if (npc != null)
                {
                    currentNPC = npc;
                    // Display a prompt to the player
                    if (promptUI)
                    {
                        promptUI.SetDisplayText("Press E to interact with " + npc.GetName());
                    }
                }
                else
                {
                    currentNPC = null;
                    if (promptUI)
                    {
                        promptUI.HideDisplay();
                    }
                }
            }
            else
            {
                currentNPC = null;
                if (promptUI)
                {
                    promptUI.HideDisplay();
                }
            }
        }
    }

    void OnInteract()
    {
        if (currentNPC != null)
        {
            currentNPC.Interact(GetComponent<PlayerQuestHandler>());
        }
    }

}
