using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public DayNightCycle dayNightScript;

#if UNITY_EDITOR
    public bool inDebug;
#endif

    private void Awake()
    {
        //Limit FPS + VSync
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 144;

        //Singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
