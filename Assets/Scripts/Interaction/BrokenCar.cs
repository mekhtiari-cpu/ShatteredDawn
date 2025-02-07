using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenCar : Interactable
{
    [SerializeField] bool playerNearCar;

    [Header("Car Parts To Be Fixed")]
    [SerializeField] int tiresReplaced;
    [SerializeField] bool hasReplacedBattery;
    [SerializeField] bool hasFilledOil;
    [SerializeField] bool hasObtainedIgnitionKey;
    [SerializeField] GameObject[] tiresToReplace;
    int lastTireReplacedIndex;
    int numPartsFixed;

    private void Update()
    {
        CloseUIIfPlayerFar();
    }

    //Car interaction
    public override void Interact()
    {
        base.Interact();
        playerNearCar = true;
        UI_Manager.instance.GetCarUI().ToggleCarInteractText();
        if(numPartsFixed >= 5)
        {
            UI_Manager.instance.GetCarUI().ChangeInteractText("Press E to drive the car and escape");
        }
    }
    public override void StopInteracting()
    {
        base.StopInteracting();
        playerNearCar = false;
        UI_Manager.instance.GetCarUI().ToggleCarInteractText();
    }

    //Returning key items to the car
    public void ReturnKeyItem(KeyItem keyItem)
    {
        numPartsFixed++;
        if(numPartsFixed >= 5)
        {
            UI_Manager.instance.GetCarUI().DisableCarInfo();
            UI_Manager.instance.GetCarUI().ChangeInteractText("Press E to drive the car and escape");
            UI_Manager.instance.GetCarUI().ToggleCarInteractText();
        }
        switch (keyItem.keyItemType)
        {
            case KeyItem.KeyItemType.Tire:
                ReplaceTire();
                break;
            case KeyItem.KeyItemType.Battery:
                ReplaceBattery();
                break;
            case KeyItem.KeyItemType.OilCan:
                FillOil();
                break;
            case KeyItem.KeyItemType.IgnitionKey:
                ObtainedIgnitionKey();
                break;
        }
        
    }

    //Replacing key items methods
    public void ReplaceTire()
    {
        tiresReplaced++;
        int randTire = Random.Range(0, 2);
        if(tiresToReplace[randTire].activeSelf == false)
        {
            tiresToReplace[randTire].SetActive(true);
        }
        else
        {
            if (lastTireReplacedIndex == 0)
            {
                tiresToReplace[1].SetActive(true);
            }
            else
            {
                tiresToReplace[0].SetActive(true);
            }
        }
    }

    public void ReplaceBattery()
    {
        hasReplacedBattery = true;
    }

    public void FillOil()
    {
        hasFilledOil = true;
    }

    public void ObtainedIgnitionKey()
    {
        hasObtainedIgnitionKey = true;
    }

    public bool GetPlayerNearCar()
    {
        return playerNearCar;
    }

    void CloseUIIfPlayerFar()
    {
        if(!playerNearCar)
        {
            UI_Manager.instance.GetCarUI().DisableCarInfo();
        }
    }

    public int GetNumPartsFixed()
    {
        return numPartsFixed;
    }

    //Return all of the car info 
    public string GetCarInfo()
    {
        PlayerQuestHandler questHandler = GameManager.instance.playerQuest;
        if (questHandler != null)
        {
            foreach (Quest quest in questHandler.activeQuests)
            {
                if (quest.isCompleted || quest.turnedIn)
                {
                    continue;
                }

                foreach (QuestCompletionCondition condition in quest.completionConditions)
                {
                    if (condition.completionType == QuestCompletionType.InteractWith)
                    {
                        condition.RegisterInteraction(this);
                        questHandler.CheckQuestCompletionConditions();
                    }

                }
            }
        }
        string tireStatus = "� "+tiresReplaced+" Tire Replaced, " + (2-tiresReplaced) + " Remaining" + "\n";
        string batteryStatus = "� Battery Replaced: " + hasReplacedBattery + "\n";
        string oilStatus = "� Has Filled Oil: " + hasFilledOil + "\n";
        string obtainedIgnitionStatus = "� Has Obtained Ignition Key: " + hasObtainedIgnitionKey + "\n";
        string carInfo = tireStatus + "\n" + batteryStatus + "\n" + oilStatus + "\n" + obtainedIgnitionStatus;
        return carInfo;
    }
}
