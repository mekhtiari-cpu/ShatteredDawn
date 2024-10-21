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
    
    #if UNITY_EDITOR
        public bool inDebug;
        public const string QUESTFILEPATH = "QuestData";

        [MenuItem("Shattered Down/Quest Data/Reset All Quest Data")]
        private static void ResetAllQuestData()
        {
            Quest[] allQuests = Resources.LoadAll(QUESTFILEPATH).Cast<Quest>().ToArray();

            foreach (Quest item in allQuests) 
            {
                item.isCompleted = false;
                item.turnedIn = false;
            }
        }
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
