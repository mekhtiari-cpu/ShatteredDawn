using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;

    private void Start()
    {
        // Initialise sliders with saved values
        masterVolumeSlider.value = GameSettingsManager.Instance.Settings.MasterVolume;
        musicVolumeSlider.value = GameSettingsManager.Instance.Settings.MusicVolume;
        effectsVolumeSlider.value = GameSettingsManager.Instance.Settings.EffectsVolume;
    }

    public void OnMasterVolumeChanged(float volume)
    {
        GameSettingsManager.Instance.SetMasterVolume(volume);
    }

    public void OnMusicVolumeChanged(float volume)
    {
        //GameSettingsManager.Instance.SetMusicVolume(volume);
    }

    public void OnEffectsVolumeChanged(float volume)
    {
        //GameSettingsManager.Instance.SetEffectsVolume(volume);
    }
}
