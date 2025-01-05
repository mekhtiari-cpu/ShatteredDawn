using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource music, environment, ui, creatures, sfx;
    [Space]
    public AudioClip mainMenuMusic, ambianceMusic, uiClick;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    #region MUSIC
    public void PlayMusic(AudioClip audio)
    {
        float volume = 1 * GameSettingsManager.Instance.Settings.MasterVolume * GameSettingsManager.Instance.Settings.MusicVolume;

        music.clip = audio;
        music.volume = volume;

        music.Play();
    }
    public void StopMusic()
    {
        if(music.isPlaying)
        {
            music.Stop();
        }     
    }
    #endregion

    #region Ambience
    public void PlayAmbience(AudioClip audio)
    {
        float volume = 1 * GameSettingsManager.Instance.Settings.MasterVolume * GameSettingsManager.Instance.Settings.EnvironmentVolume;

        environment.clip = audio;
        environment.volume = volume;

        environment.Play();
    }
    public void StopAmbience()
    {
        if (environment.isPlaying)
        {
            environment.Stop();
        }
    }
    #endregion

    public void PlayUI(AudioClip audio)
    {
        float volume = 1 * GameSettingsManager.Instance.Settings.MasterVolume * GameSettingsManager.Instance.Settings.EffectsVolume;
        ui.PlayOneShot(audio, volume);
    }
}
