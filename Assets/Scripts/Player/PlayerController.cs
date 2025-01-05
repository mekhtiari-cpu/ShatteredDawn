using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public enum playerFiringState
{
    HipFire, Ads
}

public class PlayerController : MonoBehaviour
{
    private Player_Movement pm;
    private UI_Manager ui_Manager;
    private Weapon weaponScript;
    private PlayerInputControls inputControls;
    private Player_Mouse_Look mouseLook;

    #region Variables
    [Header("General Variables")]
    public playerFiringState firingState;

    private Text ui_Ammo;

    public float damageMultiplier, score;
    public int killCounter; // Move to master/manager
    #endregion

    [Header("Data")]
    public int zombiesKilled = 0;

    private float aimAngle;
    public bool isAiming;

    private void Awake()
    {
        weaponScript = GetComponent<Weapon>();
        if (weaponScript == null)
        {
            weaponScript = gameObject.AddComponent<Weapon>();
        }

        pm = GetComponent<Player_Movement>();
        if (pm == null)
        {
            pm = gameObject.AddComponent<Player_Movement>();
        }

        inputControls = GetComponent<PlayerInputControls>();
        if (inputControls == null)
        {
            inputControls = gameObject.AddComponent<PlayerInputControls>();
        }

        ui_Manager = FindFirstObjectByType<UI_Manager>();
    }

    void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            GameSettingsManager gsm = GameSettingsManager.Instance;
            if (gsm)
            {
                if(gsm.isSettingOpen == true)
                    return;
            }

            if(FindFirstObjectByType<PauseManager>())
            {
                PauseManager pm = FindFirstObjectByType<PauseManager>();
                if (GameManager.instance.paused)
                {
                    pm.ClosePause();
                }
                else
                {
                    pm.OpenPause();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //weaponScript.RefreshAmmo(ui_Ammo);

        //weaponScript.RefreshAmmo(ui_Ammo);
    }


    public Player_Movement GetPlayerMovementScript() { return pm; }
    public UI_Manager GetUIManagerScript() { return ui_Manager; }
    public Weapon GetWeaponScript() { return weaponScript; }
    public PlayerInputControls GetPlayerInputControlsScript() { return inputControls; }
    

}
