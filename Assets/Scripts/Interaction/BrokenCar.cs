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
    [SerializeField] bool hasReplacedHeadlights;
    [SerializeField] bool hasObtainedIgnitionKey;

    private void Update()
    {
        //CloseUIIfPlayerFar();
    }

    public override void Interact()
    {
        base.Interact();
        playerNearCar = true;
        UI_Manager.instance.GetCarUI().ToggleCarInteractText();
    }
    public override void StopInteracting()
    {
        base.StopInteracting();
        playerNearCar = false;
        UI_Manager.instance.GetCarUI().ToggleCarInteractText();
    }

    public void ReturnKeyItem(KeyItem keyItem)
    {
        switch(keyItem.keyItemType)
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
            default:
                Debug.Log("Invalid key item");
                break;
        }
        
    }

    public void ReplaceTire()
    {
        Debug.Log("1 Tire Replaced, " + tiresReplaced + " Remaining");
        tiresReplaced++;
    }

    public void ReplaceBattery()
    {
        Debug.Log("Battery Replaced");
        hasReplacedBattery = true;
    }

    public void FillOil()
    {
        Debug.Log("Oil Filled");
        hasFilledOil = true;
    }

    public void ObtainedIgnitionKey()
    {
        Debug.Log("Has Obtained Ignition Key");
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
            UI_Manager.instance.GetCarUI().ToggleCarInfoText();
        }
    }

    public string GetCarInfo()
    {
        string tireStatus = "• "+tiresReplaced+" Tire Replaced, " + (2-tiresReplaced) + " Remaining" + "\n";
        string batteryStatus = "• Battery Replaced: " + hasReplacedBattery + "\n";
        string oilStatus = "• Has Filled Oil: " + hasFilledOil + "\n";
        string obtainedIgnitionStatus = "• Has Obtained Ignition Key: " + hasObtainedIgnitionKey + "\n";
        string carInfo = tireStatus + "\n" + batteryStatus + "\n" + oilStatus + "\n" + obtainedIgnitionStatus;
        return carInfo;
    }
}
