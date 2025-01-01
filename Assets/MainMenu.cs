using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private LoadingScreenManager manager;
    private GameSettingsManager settings;

    void Start() 
    {
        manager = FindObjectOfType<LoadingScreenManager>();
    }

    public void PlayGame()
    {
        manager.LoadSceneWithTransition("Main Scene");
    }

    public void Settings()
    {
        settings = GameSettingsManager.Instance;
        if (settings) 
        {
            settings.gameObject.SetActive(true);
        } 
            
    }
  
}
