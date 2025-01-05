using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public void OpenPause()
    {
        Cursor.lockState = CursorLockMode.None;
        
        Time.timeScale = 0.0f;
        if(GameManager.instance)
        {
            GameManager.instance.paused = true;
        }
        
        if(TryGetComponent<CanvasGroup>(out CanvasGroup cg))
        {
            cg.blocksRaycasts = true;
            cg.alpha = 1;
        }

    }

    public void ClosePause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;

        if (GameManager.instance)
        {
            GameManager.instance.paused = false;
        }

        if (TryGetComponent<CanvasGroup>(out CanvasGroup cg))
        {
            cg.alpha = 0;
            cg.blocksRaycasts = false;
        }
    }
    public void Resume()
    {
        if (SoundManager.Instance)
        {
            SoundManager.Instance.PlayUI(SoundManager.Instance.uiClick);
        }

        ClosePause();
    }
    public void Settings()
    {
        if(SoundManager.Instance)
        {
            SoundManager.Instance.PlayUI(SoundManager.Instance.uiClick);
        }
       
        if (GameSettingsManager.Instance)
        {
            GameSettingsManager gsm = FindFirstObjectByType<GameSettingsManager>();
            gsm.OpenSettings();
        }
    }

    public void Quit()
    {
        if (SoundManager.Instance)
        {
            SoundManager.Instance.PlayUI(SoundManager.Instance.uiClick);
        }

        if (FindFirstObjectByType<LoadingScreenManager>())
        {
            LoadingScreenManager lsm = FindFirstObjectByType<LoadingScreenManager>();
            lsm.LoadSceneWithTransition("Main Menu");
        }
    }

}
