using TMPro;
using UnityEngine;

public class ResolutionDropdown : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    private void Start()
    {
        Resolution[] resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        var options = new System.Collections.Generic.List<string>();
        foreach (var res in resolutions)
        {
            options.Add($"{res.width} x {res.height}");
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = GameSettingsManager.Instance.Settings.ResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void OnResolutionChanged(int index)
    {
        GameSettingsManager.Instance.SetResolution(index);
    }
}
