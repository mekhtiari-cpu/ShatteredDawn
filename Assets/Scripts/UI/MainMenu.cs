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
        SoundManager.Instance.PlayUI(SoundManager.Instance.uiClick);
        manager.LoadSceneWithTransition("Main Scene");
    }

    public void Settings()
    {
        SoundManager.Instance.PlayUI(SoundManager.Instance.uiClick);
        settings = GameSettingsManager.Instance;
        settings.OpenSettings();            
    }

    public void Achievements()
    {
        SoundManager.Instance.PlayUI(SoundManager.Instance.uiClick);
        // not yet implemented
    }
  
}
