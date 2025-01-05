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

    [SerializeField] HealthManager myHealth;
    [SerializeField] Player_Death myDeath;

    private IEnumerator coroutine;
    private float elpasedTime;
    private float healthDecay = 5;

    public CanvasGroup frostEffect;

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
                if (myHealth.GetCurrentHealth() <= 0f)
                {
                    myDeath.SetCauseOfDeath("You died of hypothermia.");
                }
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
        GameSettingsManager gsm = GameSettingsManager.Instance;
        float modifier = 1f;
        if (gsm)
        {
            switch (gsm.Settings.Difficulty)
            {
                case "Easy":
                    modifier = 0.8f; ;
                    break;

                case "Normal":
                    modifier = 1;
                    break;

                case "Hard":
                    modifier = 1.25f;
                    break;

                default:
                    modifier = 1;
                    break;

            }
        }
        temperatureDecayRate = baseDecayRate * modifier;
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
                    if(temperature >= 0.95f)
                    {
                        newTemp = 1;
                    }
                    else
                    {
                        float missingTemperature = 1 - temperature;
                        float recoveryRate = tempScalar * missingTemperature;
                        newTemp = temperature + recoveryRate;
                    }
                }
            }

            temperature = Mathf.Clamp(newTemp, 0, 1);

            tempBar.SetValue(temperature);
            frostEffect.alpha = 1 - temperature;

            yield return new WaitForSeconds(waitInterval);
        }

    }

    public bool GetIsNearWarmth()
    {
        return isNearWarmth;
    }
}
