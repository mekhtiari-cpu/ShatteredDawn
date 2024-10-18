using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Temperature_Manager : MonoBehaviour
{
    [SerializeField] TemperatureBar tempBar;
    [SerializeField] float temperature;
    [SerializeField] float tempDivisor;
    [SerializeField] float waitInterval;

    private IEnumerator coroutine;

    void Start()
    {
        coroutine = DecreaseTemperature();
        StartCoroutine(coroutine);
    }

    IEnumerator DecreaseTemperature()
    {
        while (temperature > 0)
        {
            float newTemp = temperature / tempDivisor;
            temperature = newTemp;
            tempBar.DecreaseValue(newTemp);
            yield return new WaitForSeconds(waitInterval);
        }
    }
}
