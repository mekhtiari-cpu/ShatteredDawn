using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private LoadingScreenManager manager;
    private GameSettingsManager settings;
    //private AchievementsManager achievements;

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
            settings.transform.GetChild(0).gameObject.SetActive(true);
        } 
            
    }

    public void Achievements()
    {
        // not yet implemented
    }
  
}
