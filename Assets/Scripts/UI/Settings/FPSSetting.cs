using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSSetting : MonoBehaviour
{
    public TMP_Dropdown fpsDropdown; // Dropdown for selecting FPS options
    public Toggle vsyncToggle;      // Optional toggle for VSync (linked to disable it when needed)

    private string[] options = { "30 FPS", "60 FPS", "Unlimited" };

    private void Start()
    {
        // Populate dropdown with FPS options
        fpsDropdown.ClearOptions();
        fpsDropdown.AddOptions(new System.Collections.Generic.List<string>(options));

        // Set initial value based on current settings
        int initialIndex = GameSettingsManager.Instance.Settings.TargetFPS switch
        {
            30 => 0,
            60 => 1,
            -1 => 2, // Unlimited FPS
            _ => 1,  // Default to 60 FPS if unexpected value
        };
        fpsDropdown.value = initialIndex;
        fpsDropdown.RefreshShownValue();

        // Update VSync toggle state based on settings
        if (vsyncToggle != null)
        {
            vsyncToggle.isOn = GameSettingsManager.Instance.Settings.VSync;
        }
    }

    public void OnFPSDropdownChanged(int index)
    {
        // Update FPS settings based on selected dropdown value
        switch (index)
        {
            case 0: // 30 FPS
                GameSettingsManager.Instance.SetTargetFPS(30);
                DisableVSync();
                break;

            case 1: // 60 FPS
                GameSettingsManager.Instance.SetTargetFPS(60);
                DisableVSync();
                break;

            case 2: // Unlimited FPS
                GameSettingsManager.Instance.SetTargetFPS(-1); // -1 means unlimited in Unity
                break;
        }
    }

    private void DisableVSync()
    {
        // Disable VSync if present, as FPS limit conflicts with it
        if (vsyncToggle != null)
        {
            vsyncToggle.isOn = false;
            GameSettingsManager.Instance.SetVSync(false);
        }
    }
}
