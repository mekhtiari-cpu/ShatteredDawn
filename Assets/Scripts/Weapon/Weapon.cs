using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Weapon : MonoBehaviour
{
    public Gun loadOut;
    [HideInInspector] public Gun currentGunData;

    public GameObject currentEquipment;

    public Transform weaponParent;

    public GameObject bulletHolePrefab;
    public LayerMask canBeShot;

    private float currentCooldown;
    private bool isReloading;
    public bool pickUp;

    PlayerController player;

    private Transform anchor;
    private Transform stateAds;
    private Transform stateHip;

    public AudioSource sfx;

    private Transform ui_HitMarker;

    private RawImage hitMarkerImage;
    private float hitMarkerWait;
    public AudioClip hitMarkerSound;

    private Color CLEARWHITE = new Color(1, 1, 1, 0);

    bool fireShots;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        weaponParent = transform.Find("Weapons");
        sfx = transform.Find("Sounds/GunShots").GetComponent<AudioSource>();
        Equip();
        anchor = currentEquipment.transform.Find("Anchors");
        stateAds = currentEquipment.transform.Find("States/Ads");
        stateHip = currentEquipment.transform.Find("States/Hip");

        loadOut.Initialize();

        ui_HitMarker = GameObject.Find("HUD/HitMarker").transform;
        hitMarkerImage = ui_HitMarker.transform.Find("Image").GetComponent<RawImage>();
        hitMarkerImage.color = CLEARWHITE;
            
    }

    // Update is called once per frame
    void Update()
    {
        //if (Pause.paused) return;

        if (ui_HitMarker != null)
        {
            if (hitMarkerWait > 0)
            {
                hitMarkerWait -= Time.deltaTime;
            }
            else if (hitMarkerImage.color.a > 0)
            {
                hitMarkerImage.color = Color.Lerp(hitMarkerImage.color, CLEARWHITE, Time.deltaTime * 2f);
            }
        }
            
        if (currentEquipment != null)
        {
            #region Semi Fire Mode
            if (loadOut.firingMode == GunFiringTypes.SemiAuto)
            {
                if (fireShots && currentCooldown <= 0)
                {
                    if (currentGunData.FireBullet())
                    {
                        Shoot();
                        fireShots = false;
                        //Play Shoot Animation here
                    }
                    else
                    {
                        StartCoroutine(Reload(loadOut.reloadTime));
                    }
                }
            }
            #endregion
            #region Auto Fire Mode
            if (loadOut.firingMode == GunFiringTypes.Automatic)
            {
                if (fireShots && currentCooldown <= 0)
                {
                    if (currentGunData.FireBullet())
                    {
                        Shoot();
                        //Play Shoot Animation here
                    }
                    else
                    {
                        StartCoroutine(Reload(loadOut.reloadTime));
                    }
                }
            }
            #endregion

            Aim(player.isAiming);
            #region Cooldown
            //cooldown
            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;
            }
            #endregion
                     
            //weapon elasticity
            currentEquipment.transform.localPosition = Vector3.Lerp(currentEquipment.transform.localPosition, Vector3.zero, Time.deltaTime * 4f);
        }
    }
    #region Functions/Methods
    void Equip()
    {
        if (currentEquipment != null)
        {
            if (isReloading)
            {
                StopCoroutine("Reload");
            }
            Destroy(currentEquipment);
        }

        GameObject newEquipment = Instantiate(loadOut.prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
        newEquipment.transform.localPosition = Vector3.zero;
        newEquipment.transform.localEulerAngles = Vector3.zero;
        newEquipment.GetComponent<GunSway>().isMine = true;

        ChangeLayerRecursivly(newEquipment, 8);

        currentEquipment = newEquipment;
        currentGunData = loadOut;
    }
    private void ChangeLayerRecursivly(GameObject target, int layer)
    {
        target.layer = layer;
        foreach (Transform t in target.transform)
        {
            ChangeLayerRecursivly(t.gameObject, layer);
        }
    }
    void PickupWeapon(string name)
    {
        //find weapon           
        Gun newWeapon = GunLibrary.FindGun(name);

        //add weapon


        if (loadOut == null)
        {
            pickUp = true;
            loadOut = newWeapon;
            Equip();
            currentGunData.Initialize();
        }
        else if (newWeapon.name == currentGunData.name)
        {
            if ((currentGunData.ammo != currentGunData.GetStash()) || (currentGunData.clipSize != currentGunData.GetClip()))
            {
                pickUp = true;
                currentGunData.Initialize();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            pickUp = true;
            loadOut = newWeapon;
            Equip();
            currentGunData.Initialize();
            //Drop current Weapon
        }
    }
    public bool Aim(bool playerIsAiming)
    {
        if (!currentEquipment)
        {
            return false;
        }
        if (isReloading)
        {
            playerIsAiming = false;
        }
        anchor = currentEquipment.transform.Find("Anchors");
        stateAds = currentEquipment.transform.Find("States/Ads");
        stateHip = currentEquipment.transform.Find("States/Hip");

        player = GetComponent<PlayerController>();

        player.isAiming = playerIsAiming;
        if (playerIsAiming)
        {
            //ads               
            player.firingState = playerFiringState.Ads;

            anchor.position = Vector3.Lerp(anchor.position, stateAds.position, Time.deltaTime * loadOut.aimSpeed);
        }
        else
        {
            //hip
            player.firingState = playerFiringState.HipFire;

            anchor.position = Vector3.Lerp(anchor.position, stateHip.position, Time.deltaTime * loadOut.aimSpeed);
        }
        return playerIsAiming;
    }
    void Shoot()
    {
        Transform temp_spawnPoint = transform.Find("Cameras/Main Camera");

        //cooldown
        currentCooldown = loadOut.fireRate;

        for (int i = 0; i < Mathf.Max(1, loadOut.pellets); i++)
        {
            //Setup Bloom
            Vector3 temp_Bloom = temp_spawnPoint.position + temp_spawnPoint.forward * 1000f;

            //Bloom
            temp_Bloom += Random.Range(-loadOut.bloom, loadOut.bloom) * temp_spawnPoint.up;
            temp_Bloom += Random.Range(-loadOut.bloom, loadOut.bloom) * temp_spawnPoint.right;
            temp_Bloom -= temp_spawnPoint.position;
            temp_Bloom.Normalize();


            //Raycast
            RaycastHit temp_hit = new RaycastHit();
            if (Physics.Raycast(temp_spawnPoint.position, temp_Bloom, out temp_hit, 1000f, canBeShot))
            {              
                // Change this to be layer for enemies
                if (temp_hit.collider.gameObject.layer == 11)
                {
                    // @note nathanael.hondi 25/11/24. Give Damage

                    //Hit Marker
                    hitMarkerImage.color = Color.white;
                    sfx.PlayOneShot(hitMarkerSound);
                    hitMarkerWait = 1f;
                }
                else
                {
                    Vector3 bulletHolePosition = temp_hit.point + temp_hit.normal * 0.001f;

                    // Align the bullet hole to the surface
                    Quaternion bulletHoleRotation = Quaternion.LookRotation(temp_hit.normal);

                    GameObject newHole = Instantiate(bulletHolePrefab, bulletHolePosition, bulletHoleRotation);

                    // Optional: Parent to the hit object for dynamic surfaces
                    newHole.transform.SetParent(temp_hit.collider.transform);

                    // Destroy after 2 seconds
                    // @note nathanael.hondi 31/12/24. Pooling for extra efficiency and less garbage collection
                    Destroy(newHole, 2f);
                }           
            }
        }
        //Sound
        sfx.clip = currentGunData.gunShotSound;
        sfx.pitch = currentGunData.basepitch - currentGunData.pitchRandomisation + Random.Range(-currentGunData.pitchRandomisation, currentGunData.pitchRandomisation);
        sfx.volume = currentGunData.gunSoundVolume;
        sfx.PlayOneShot(sfx.clip);

        //Gun FX
        if (player.firingState == playerFiringState.Ads)
        {
            currentEquipment.transform.Rotate(-loadOut.recoil / 2, 0, 0);
        }
        else
        {
            currentEquipment.transform.Rotate(-loadOut.recoil, 0, 0);
        }
        currentEquipment.transform.position -= currentEquipment.transform.forward * loadOut.kickBack;
        if (currentGunData.recovery)
        {
            // @note nathanael.hondi 25/11/24. Play recovery animation
        }
    }

    IEnumerator Reload(float reloadTime)
    {
        isReloading = true;
        currentEquipment.SetActive(false);
        yield return new WaitForSeconds(reloadTime);
        currentEquipment.SetActive(true);
        currentGunData.Reload();
        isReloading = false;
    }

    #region New Input System
    void OnReload(InputValue value)
    {
        if (value.isPressed)
        {
            if(loadOut)
            {
                StartCoroutine(Reload(loadOut.reloadTime));
            }
        }
    }

    void OnShoot(InputValue value)
    {
        if(loadOut)
        {
            fireShots = value.isPressed;
        }   
    }

    void OnAim(InputValue value)
    {
        player.isAiming = value.isPressed; // Assign directly
    }
    void OnDropWeapon(InputValue value)
    {
        if (value.isPressed)
        {
            // Drop Weapon
        }
    }
    #endregion

    private void TakeDamage(int damage)
    {
        // @note nathanael.hondi 25/11/24. Update player health
    }

    public void RefreshAmmo(Text ammoText)
    {
        int clip = currentGunData.GetClip();
        int stash = currentGunData.GetStash();

        ammoText.text = clip.ToString("00") + " / " + stash.ToString("00");
    }

    #endregion    

}
