using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarUI : MonoBehaviour
{
    [SerializeField] GameObject carInteractText;
    [SerializeField] GameObject carInfo;
    [SerializeField] TMP_Text carInfoText;
    [SerializeField] BrokenCar brokenCar;

    public void ToggleCarInteractText()
    {
        carInteractText.SetActive(!carInteractText.activeSelf);
    }

    public void ToggleCarInfoText()
    {
        carInfo.SetActive(!carInfo.activeSelf);
        ToggleCarInteractText();
        carInfoText.text = brokenCar.GetCarInfo();
    }
}