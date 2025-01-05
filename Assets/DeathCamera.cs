using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCamera : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;

    public void ShowDeathPanel()
    {
        deathPanel.SetActive(true);
    }
}
