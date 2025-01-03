using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private float aimAngle;
    public bool isAiming;

    private void Awake()
    {
        weaponScript = GetComponent<Weapon>();
        if (weaponScript == null)
        {
            Debug.LogWarning("Weapon component not found. Adding a new component. Likely to cause problems");
            weaponScript = gameObject.AddComponent<Weapon>();
        }

        pm = GetComponent<Player_Movement>();
        if (pm == null)
        {
            Debug.LogWarning("Player_Movement component not found.  Adding a new component. Likely to cause problems");
            pm = gameObject.AddComponent<Player_Movement>();
        }

        inputControls = GetComponent<PlayerInputControls>();
        if (inputControls == null)
        {
            Debug.LogWarning("PlayerInputControls component not found.  Adding a new component. Likely to cause problems");
            inputControls = gameObject.AddComponent<PlayerInputControls>();
        }

        ui_Manager = FindFirstObjectByType<UI_Manager>();
        if (ui_Manager == null)
        {
            Debug.LogWarning("UI_Manager not found.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
