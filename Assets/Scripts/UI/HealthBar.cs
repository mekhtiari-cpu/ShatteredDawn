using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{

    // variable to store the slider
    public Slider slider;

    public void setMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health; // sliders should start at max health 

    }

    public void setHealth(float health)
    {
        slider.value = health;
    }
}
