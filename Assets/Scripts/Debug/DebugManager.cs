using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public static DebugManager instance { get; private set; }

    private void Awake()
    {
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

    private void Start()
    {
        gameObject.SetActive(GameManager.instance.inDebug);
    }

    // All Debug Actions
    public void AddDay(int val)
    {
        DayNightCycle script = GameManager.instance.dayNightScript;
        script.IncrementDay(val);
    }

    public void SetToMorning()
    {
        DayNightCycle script = GameManager.instance.dayNightScript;
        script.GetMorningTime();
    }

    public void SetToAfternoon()
    {
        DayNightCycle script = GameManager.instance.dayNightScript;
        script.GetAfternoonTime();
    }

    public void SetToEvening()
    {
        DayNightCycle script = GameManager.instance.dayNightScript;
        script.GetEveningTime();
    }

    public void SetToNight()
    {
        DayNightCycle script = GameManager.instance.dayNightScript;
        script.GetNightTime();
    }

    public void CompleteAllQuests()
    {
        PlayerQuestHandler questHandler = GameManager.instance.playerQuest;
        questHandler.CompleteAllActiveQuests();
    }

    public void ResetAllQuestData()
    {
        GameManager.ResetAllQuestData();
    }
}
