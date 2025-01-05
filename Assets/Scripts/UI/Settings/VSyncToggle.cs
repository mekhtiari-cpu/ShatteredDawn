using UnityEngine;
using UnityEngine.UI;

public class VSyncToggle : MonoBehaviour
{
    public Toggle vsyncToggle;

    private void Start()
    {
        // Initialise the toggle with the current VSync setting
        vsyncToggle.isOn = GameSettingsManager.Instance.Settings.VSync;
    }

    public void OnVSyncToggleChanged()
    {
        GameSettingsManager.Instance.SetVSync(!vsyncToggle.isOn);
    }
}
