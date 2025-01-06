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
    
    public const string QUESTFILEPATH = "QuestData";

    public bool paused;

    public Transform player;

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

            foreach (QuestCompletionCondition condition in item.completionConditions)
            {
                condition.Reset();
            }
        }
    }

    private void Awake()
    {
        ResetAllQuestData();
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
        }
    }

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>().transform;
    }
}
