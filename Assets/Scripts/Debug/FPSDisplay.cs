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
            Debug.LogError("FPSDisplay: TMP_Text component is not assigned or missing.");
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
            Debug.LogError("FPSDisplay: TMP_Text component is not assigned or missing.");
            return;
        }

        // Enable or disable the FPS display based on settings
        fpsText.enabled = gsm.Settings.DisplayFPS;
    }
}
