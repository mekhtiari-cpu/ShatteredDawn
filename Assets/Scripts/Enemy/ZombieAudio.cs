using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAudio : MonoBehaviour
{
    [SerializeField] AudioSource idleAudio;
    [SerializeField] AudioSource playerSeenAudio;
    [SerializeField] AudioSource chaseAudio;
    [SerializeField] AudioSource hurtAudio;
    [SerializeField] AudioSource deathAudio;

    public void PlayIdleAudio()
    {
        GameSettingsManager gsm = GameSettingsManager.Instance;
        idleAudio.volume = gsm ? 1 * gsm.Settings.MasterVolume * gsm.Settings.HostileVolume : 1f;
        idleAudio.Play();
    }
    public void StopPlayingIdleAudio()
    {
        idleAudio.Stop();
    }

    public void PlayPlayerSeenAudio()
    {
        GameSettingsManager gsm = GameSettingsManager.Instance;
        playerSeenAudio.volume = gsm ? 1 * gsm.Settings.MasterVolume * gsm.Settings.HostileVolume : 1f;
        playerSeenAudio.Play();
    }

    public void PlayChaseAudio()
    {
        GameSettingsManager gsm = GameSettingsManager.Instance;
        chaseAudio.volume = gsm ? 1 * gsm.Settings.MasterVolume * gsm.Settings.HostileVolume : 1f;
        chaseAudio.Play();
    }
    public void StopPlayingChaseAudio()
    {
        chaseAudio.Stop();
    }

    public void PlayHurtAudio()
    {
        GameSettingsManager gsm = GameSettingsManager.Instance;
        hurtAudio.volume = gsm ? 1 * gsm.Settings.MasterVolume * gsm.Settings.HostileVolume : 1f;
        hurtAudio.Play();
    }

    public void PlayDeathAudio()
    {
        GameSettingsManager gsm = GameSettingsManager.Instance;
        deathAudio.volume = gsm ? 1 * gsm.Settings.MasterVolume * gsm.Settings.HostileVolume : 1f;
        deathAudio.Play();
    }
}
