using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("Gameplay Elements")]
    public PlayerQuestHandler playerQuest;
    public UIHandlerManager UIHandler;
    [Space]
    public DayNightCycle dayNightScript;
    
    public bool inDebug;
    public const string QUESTFILEPATH = "QuestData";

    // Reset Scriptable Objects so all quest appear as incomplete
#if UNITY_EDITOR
    [MenuItem("Shattered Down/Quest Data/Reset All Quest Data")]
#endif
    public static void ResetAllQuestData()
    {
        Quest[] allQuests = Resources.LoadAll(QUESTFILEPATH).Cast<Quest>().ToArray();

        foreach (Quest item in allQuests) 
        {
            item.isCompleted = false;
            item.turnedIn = false;
        }
    }

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
