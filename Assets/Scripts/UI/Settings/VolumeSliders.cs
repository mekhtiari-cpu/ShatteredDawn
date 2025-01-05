using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;
    public Slider uiSoundVolumeSlider;
    public Slider hostileVolumeSlider;
    public Slider environmentVolumeSlider;

    private void Start()
    {
        masterVolumeSlider.value = GameSettingsManager.Instance.Settings.MasterVolume;
        musicVolumeSlider.value = GameSettingsManager.Instance.Settings.MusicVolume;
        effectsVolumeSlider.value = GameSettingsManager.Instance.Settings.EffectsVolume;
        uiSoundVolumeSlider.value = GameSettingsManager.Instance.Settings.UISoundVolume;
        hostileVolumeSlider.value = GameSettingsManager.Instance.Settings.HostileVolume;
        environmentVolumeSlider.value = GameSettingsManager.Instance.Settings.EnvironmentVolume;
    }

    public void OnMasterVolumeChanged(float volume)
    {
        GameSettingsManager.Instance.SetMasterVolume(volume);
    }

    public void OnMusicVolumeChanged(float volume)
    {
        GameSettingsManager.Instance.SetMusicVolume(volume);
    }

    public void OnEffectsVolumeChanged(float volume)
    {
        GameSettingsManager.Instance.SetEffectsVolume(volume);
    }

    public void OnUISoundVolumeChanged(float volume)
    {
        GameSettingsManager.Instance.SetUISoundVolume(volume);
    }

    public void OnHostileVolumeChanged(float volume)
    {
        GameSettingsManager.Instance.SetHostileVolume(volume);
    }

    public void OnEnvironmentVolumeChanged(float volume)
    {
        GameSettingsManager.Instance.SetEnvironmentVolume(volume);
    }
}
