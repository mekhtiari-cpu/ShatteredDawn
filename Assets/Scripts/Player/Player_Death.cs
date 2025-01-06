using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Death : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera deathCamera;

    [Header("Components")]
    [SerializeField] Player_Movement pm;
    [SerializeField] Player_Mouse_Look pml;

    [Header("Other references")]
    [SerializeField] GameManager gm;
    [SerializeField] GameObject hud;
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject frostEffect;
    [SerializeField] TMP_Text deathText; 

    //Method for when the player dies
    void Die()
    {
        mainCamera.gameObject.SetActive(false);
        deathCamera.gameObject.SetActive(true);
        frostEffect.SetActive(false);
        hud.SetActive(false);
        pm.enabled = false;
        pml.enabled = false;
        gm.gameObject.SetActive(false);
    }

    public void SetCauseOfDeath(string cause)
    {
        deathText.text = cause;
    }
}
