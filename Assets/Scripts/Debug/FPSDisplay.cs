using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    private TextMeshProUGUI fpsText;
    private float deltaTime;

    public GameSettingsManager gsm;

    private void Awake()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
        if (fpsText == null)
        {
            return;
        }
        UpdateDisplayState();
    }

    private void Update()
    {
        if (gsm.Settings.DisplayFPS && fpsText != null)
        {
            deltaTime += (Time.deltaTime - deltaTime);
            float fps = 1.0f / deltaTime;
            fpsText.text = $"FPS: {Mathf.Ceil(fps)}";
        }
    }

    public void UpdateDisplayState()
    {
        if (fpsText == null)
        {
            return;
        }

        // Enable or disable the FPS display based on settings
        fpsText.enabled = gsm.Settings.DisplayFPS;
    }
}
