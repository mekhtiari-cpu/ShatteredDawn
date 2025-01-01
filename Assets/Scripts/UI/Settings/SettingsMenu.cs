using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public void OnColorBlindModeChanged(int index)
    {
        string[] modes = { "Normal", "Protanopia", "Deuteranopia", "Tritanopia" };
        GameSettingsManager.Instance.SetColorBlindMode(modes[index]);
    }

    public void OnMasterVolumeChanged(float volume)
    {
        GameSettingsManager.Instance.SetMasterVolume(volume);
    }
}
