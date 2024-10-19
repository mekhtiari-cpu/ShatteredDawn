using UnityEngine;
using UnityEngine.UIElements;

public class DayNightCycle : MonoBehaviour
{
    [System.Serializable]
    public struct SkyboxGradient
    {
        public Color topColor;
        public Color middleColor;
        public Color bottomColor;

        public SkyboxGradient(Color top, Color middle, Color bottom)
        {
            topColor = top;
            middleColor = middle;
            bottomColor = bottom;
        }
    }

    [Header("Skybox Materials")]
    [Tooltip("Skybox Material")]
    public Material skyboxMaterial;

    [Header("Skybox Gradients")]
    public SkyboxGradient morningGradient;
    public SkyboxGradient afternoonGradient;
    public SkyboxGradient eveningGradient;
    public SkyboxGradient nightGradient;

    [Header("Light Source")]
    public Light directionalLight;
    [Space]

    [Tooltip("The total duration of a day in seconds.")]
    [SerializeField] private float dayDuration = 120f;

    [Tooltip("The total time progression through the day")]
    [SerializeField] private float timeOfDay = 0f; 

    [Tooltip("The total number of days that have passed")]
    [SerializeField] private int daysPassed = 0; 

    private float rotationSpeed;
    [SerializeField] private bool isDaytime;
    private const float SPEED_MULTIPLIER = 5f;

    private void Awake()
    {
        if(skyboxMaterial)
        {
            if(RenderSettings.skybox != skyboxMaterial)
            {
                RenderSettings.skybox = skyboxMaterial;
                Debug.LogWarning($"Changed Skybox to be one ({skyboxMaterial.name}) necessary for DayNightCycle.cs to run correctly");
            }
        }

        if(directionalLight)
        {
            if(RenderSettings.sun != directionalLight)
            {
                RenderSettings.sun = directionalLight;
                Debug.LogWarning($"Changed Sun Source to be one ({directionalLight.name}) necessary for DayNightCycle.cs to run correctly");
            }
        }
    }
    void Start()
    {
        rotationSpeed = 360f / dayDuration;

        directionalLight.transform.rotation = Quaternion.Euler(15, 0, 0);

        UpdateSkybox();
    }

    void FixedUpdate()
    {
        UpdateSunPosition();
        UpdateTimeOfDay();
    }

    void UpdateSunPosition()
    {
        // Rotate the directional light around the X-axis to simulate the sun's movement
        directionalLight.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);

        // Get the current rotation angle of the sun (the light source)
        float sunRotationX = Mathf.Repeat(directionalLight.transform.eulerAngles.x, 360f);

        // Adjust the intensity based on the sun's rotation
        if (sunRotationX <= 90f || sunRotationX >= 325f) // 345 to 90 degrees
        {
            directionalLight.intensity = Mathf.Lerp(0f, 1f, (sunRotationX < 90f ? (sunRotationX / 90f) : (sunRotationX - 325f) / 125f));
        }
        else if (sunRotationX <= 250f) // 90 to 250 degrees
        {
            directionalLight.intensity = Mathf.Lerp(1f, 0.5f, (sunRotationX - 90f) / 160f);
        }
        else if (sunRotationX <= 325f) // 250 to 345 degrees
        {
            directionalLight.intensity = 0;
        }
    }

    void UpdateTimeOfDay()
    {
        timeOfDay += Time.deltaTime;

        if (timeOfDay >= dayDuration)
        {
            // Reset timer as we enter the next day
            timeOfDay = 0f;
            daysPassed++;
        }

        if (timeOfDay < dayDuration * 0.5f)
        {
            isDaytime = true;
        }
        else
        {
            isDaytime = false;
        }

        UpdateSkybox();
    }
    void UpdateSkybox()
    {
        float timeRatio = timeOfDay / dayDuration;

        // Determine which gradient to lerp between based on the time of day
        SkyboxGradient currentGradient;

        if (timeRatio < 0.25f)
        {
            currentGradient = LerpGradient(morningGradient, afternoonGradient, (timeRatio) * SPEED_MULTIPLIER);
        }
        else if (timeRatio < 0.5f)
        {
            currentGradient = LerpGradient(afternoonGradient, eveningGradient, (timeRatio - 0.25f) * SPEED_MULTIPLIER);
        }
        else if (timeRatio < 0.75f)
        {
            currentGradient = LerpGradient(eveningGradient, nightGradient, (timeRatio - 0.5f) * SPEED_MULTIPLIER);
        }
        else
        {
            currentGradient = LerpGradient(nightGradient, morningGradient, (timeRatio - 0.75f) * SPEED_MULTIPLIER);     
        }

        // Apply the lerped colors to the skybox material
        skyboxMaterial.SetColor("_TopColor", currentGradient.topColor);
        skyboxMaterial.SetColor("_MiddleColor", currentGradient.middleColor);
        skyboxMaterial.SetColor("_BottomColor", currentGradient.bottomColor);
    }

    SkyboxGradient LerpGradient(SkyboxGradient a, SkyboxGradient b, float t)
    {
        return new SkyboxGradient(
            Color.Lerp(a.topColor, b.topColor, t),
            Color.Lerp(a.middleColor, b.middleColor, t),
            Color.Lerp(a.bottomColor, b.bottomColor, t)
        );
    }

    public int GetCurrentDay()
    {
        return daysPassed;
    }

    public bool GetIsDay()
    {
        return isDaytime;
    }
}
