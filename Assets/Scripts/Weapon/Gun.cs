using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GunFiringTypes
{
    SemiAuto, Automatic, Burst
}
public enum GunTypes
{
    AssauultRifle, SubmachhineGun, Shotgun, MachineGun, SniperRifle, Pistol, Explosive
}

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    [Header("General")]
    public int itemId;
    public string name;
    public GameObject prefab;
    public GameObject displayPrefab;
    public GunTypes gunType;
    public GunFiringTypes firingMode;

    [Header("Ammo/Mag")]
    public int ammo;
    public int clipSize;
    public int pellets; //shotgun

    [Header("Gun Var")]
    public float bloom; //Accuracy
    public float recoil;
    public float kickBack;
    public float aimSpeed;

    public float reloadTime; //Seconds
    public bool recovery; //animation (pump,, barrel etc)

    public float fireRate;
    public int damage;

    private int stash; //current ammo
    private int clip; //current clip

    [Header("Ads Var")]
    [Range(0, 1)] public float mainFOV;
    [Range(0, 1)] public float weaponFOV;

    [Header("Sound")]
    public AudioClip gunShotSound;
    public float pitchRandomisation;
    public float basepitch;
    [Range(0, 1)] public float gunSoundVolume;


    public void Initialize()
    {
        stash = ammo;
        clip = clipSize;
    }

    public bool FireBullet()
    {
        if (clip > 0)
        {
            clip -= 1;
            //stash--;

            return true;
        }
        else return false;
    }

    public void Reload()
    {
        stash += clip;
        clip = Mathf.Min(clipSize, stash);
        stash -= clip;
    }
    public int GetStash()
    {
        return stash;
    }
    public int GetClip()
    {
        return clip;
    }
}