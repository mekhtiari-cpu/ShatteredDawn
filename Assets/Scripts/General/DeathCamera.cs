using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCamera : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;

    public void ShowDeathPanel()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        deathPanel.SetActive(true);
    }
}
