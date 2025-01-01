using UnityEngine;
using UnityEngine.UI;

public class FPSToggle : MonoBehaviour
{
    public Toggle fpsToggle;

    private void Start()
    {
        // Ensure the toggle is set to the current DisplayFPS state
        fpsToggle.isOn = GameSettingsManager.Instance.Settings.DisplayFPS;
    }

    public void OnFPSToggleChanged(bool enabled)
    {
        // Update the GameSettings
        GameSettingsManager.Instance.Settings.DisplayFPS = enabled;
        GameSettingsManager.Instance.Settings.SaveSettings();

        var fpsDisplay = FindObjectOfType<FPSDisplay>();
        if (fpsDisplay != null)
        {
            fpsDisplay.UpdateDisplayState();
        }
        else
        {
            Debug.LogError("FPSToggle: FPSDisplay script is not found in the scene.");
        }
    }
}


