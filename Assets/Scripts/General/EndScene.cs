using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    [SerializeField] GameObject endPanel;

    public void ShowEndPanel()
    {
        endPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
