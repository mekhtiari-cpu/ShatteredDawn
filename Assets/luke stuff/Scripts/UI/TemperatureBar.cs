using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    public void DecreaseValue(float percentage)
    {
        healthSlider.value = percentage;
    }
}
