using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Temperature_Manager : MonoBehaviour
{
    public static Player_Temperature_Manager instance { get; private set; }

    [SerializeField] TemperatureBar tempBar;
    [SerializeField] float temperature;
    float baseDecayRate = 0.027f;
    [SerializeField] float temperatureDecayRate;
    [SerializeField] float tempScalar;
    [SerializeField] float waitInterval;
    [SerializeField] bool isNearWarmth;
    [SerializeField] GameObject deathCamera;
    [SerializeField] GameObject deathPanel;

    private IEnumerator coroutine;

    private void Awake()
    {
        //Singleton
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

    public void SetTempDecayRate(float newDecayRate)
    {
        temperatureDecayRate = newDecayRate;
    }

    public float GetDecayRate()
    {
        return temperatureDecayRate;
    }

    public void ResetDecayRate()
    {
        temperatureDecayRate = baseDecayRate;
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
        deathCamera.SetActive(true);
        deathPanel.SetActive(true);
        Destroy(this.gameObject);
    }
}
