using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Temperature_Manager : MonoBehaviour
{
    [SerializeField] TemperatureBar tempBar;
    [SerializeField] float temperature;
    [SerializeField] float tempDivisor;
    [SerializeField] float tempScalar;
    [SerializeField] float waitInterval;
    [SerializeField] bool isNearWarmth;

    private IEnumerator coroutine;

    void Start()
    {
        coroutine = ManageTemperature();
        StartCoroutine(coroutine);
    }

    public void SetWarmthStatus(bool state, float newTempScalar)
    {
        tempScalar = newTempScalar;
        isNearWarmth = state;
    }

    IEnumerator ManageTemperature()
    {
        float newTemp = temperature;
        while (true)
        {
            if (!isNearWarmth)
            {
                newTemp = temperature / tempDivisor;
            }
            else
            {
                newTemp = temperature * tempScalar;
            }

            temperature = newTemp;

            if (temperature < 0)
                temperature = 0;
            if (temperature > 1)
                temperature = 1;

            tempBar.SetValue(newTemp);

            yield return new WaitForSeconds(waitInterval);
        }
    }
}
