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
    [SerializeField] bool isNearCampfire;
    [SerializeField] GameObject deathCamera;
    [SerializeField] GameObject deathPanel;

    private IEnumerator coroutine;
    private float elpasedTime;
    private float healthDecay = 5;

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

    void Update()
    {
        if (temperature <= 0) 
        {
            if (elpasedTime > 0) 
            {
            elpasedTime -= Time.deltaTime;
            }
            else 
            {
            elpasedTime = healthDecay;
            gameObject.SendMessage("TakeDamage", 5);
            }
        }
    }

    public void SetWarmthStatus(bool state, bool setIsNearCampfire, float newTempScalar)
    {
        tempScalar = newTempScalar;
        isNearCampfire = setIsNearCampfire;
        isNearWarmth = state;
    }

    public bool GetIsNearCampfire()
    {
        return isNearCampfire;
    }

    public void SetTempDecayRate(float newDecayRate)
    {
        temperatureDecayRate = newDecayRate;
    }

    public float GetDecayRate()
    {
        return temperatureDecayRate;
    }

    public float GetTempScalar()
    {
        return tempScalar;
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
                if(temperature <= 0)
                {
                    newTemp = temperature + 0.01f;
                }
                else
                {
                    float missingTemperature = 1 - temperature;
                    float recoveryRate = tempScalar * missingTemperature;
                    newTemp = temperature + recoveryRate;
                }
            }

            temperature = newTemp;

            if (temperature <= 0)
            {
                temperature = 0;
            }
            if (temperature > 1)
            {
                temperature = 1;
            }

            tempBar.SetValue(temperature);

            yield return new WaitForSeconds(waitInterval);
        }

    }

    public bool GetIsNearWarmth()
    {
        return isNearWarmth;
    }

    public void Die()
    {
        deathCamera.SetActive(true);
        deathPanel.SetActive(true);
        Destroy(this.gameObject);
    }
}
