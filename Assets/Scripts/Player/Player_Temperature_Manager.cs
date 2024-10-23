using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Temperature_Manager : MonoBehaviour
{
    [SerializeField] TemperatureBar tempBar;
    [SerializeField] float temperature;
    [SerializeField] float temperatureDecayRate;
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
        while (true)
        {
            float newTemp;
            if (!isNearWarmth)
            {
                newTemp = temperature - temperatureDecayRate;
            }
            else
            {
                newTemp = temperature * tempScalar;
            }

            temperature = newTemp;

            if (temperature <= 0)
            {
                temperature = 0;
                break;
            }
            if (temperature > 1)
            {
                temperature = 1;
            }

            tempBar.SetValue(newTemp);

            yield return new WaitForSeconds(waitInterval);
        }

        if(temperature <= 0)
            Die();
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
