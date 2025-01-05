using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarEscapePanel : MonoBehaviour
{
    public void LoadEndScreen()
    {
        SceneManager.LoadScene("End Scene");
    }
}
