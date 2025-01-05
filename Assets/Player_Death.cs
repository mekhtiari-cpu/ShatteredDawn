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
    [SerializeField] GameObject hud;
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject frostEffect;
    [SerializeField] TMP_Text deathText; 


    void Die()
    {
        Debug.Log("Player died");
        mainCamera.gameObject.SetActive(false);
        deathCamera.gameObject.SetActive(true);
        frostEffect.SetActive(false);
        hud.SetActive(false);
        pm.enabled = false;
        pml.enabled = false;
    }

    public void SetCauseOfDeath(string cause)
    {
        deathText.text = cause;
    }
}
