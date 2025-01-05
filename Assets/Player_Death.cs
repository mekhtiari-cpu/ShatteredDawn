using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Death : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera deathCamera;

    [Header("Components")]
    [SerializeField] Player_Movement pm;
    [SerializeField] Player_Mouse_Look pml;

    void Die()
    {
        Debug.Log("Player died");
        mainCamera.gameObject.SetActive(false);
        deathCamera.gameObject.SetActive(true);
        pm.enabled = false;
        pml.enabled = false;
    }
}
