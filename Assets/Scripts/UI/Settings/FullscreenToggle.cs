using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggle : MonoBehaviour
{
    public Toggle fullscreenToggle;

    private void Start()
    {
        fullscreenToggle.isOn = GameSettingsManager.Instance.Settings.Fullscreen;
    }

    public void OnFullscreenToggleChanged(bool isFullscreen)
    {
        GameSettingsManager.Instance.SetFullscreen(isFullscreen);
    }
}
