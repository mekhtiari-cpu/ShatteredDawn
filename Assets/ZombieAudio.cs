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
        idleAudio.Play();
    }
    public void StopPlayingIdleAudio()
    {
        idleAudio.Stop();
    }

    public void PlayPlayerSeenAudio()
    {
        playerSeenAudio.Play();
    }

    public void PlayChaseAudio()
    {
        chaseAudio.Play();
    }
    public void StopPlayingChaseAudio()
    {
        chaseAudio.Stop();
    }

    public void PlayHurtAudio()
    {
        hurtAudio.Play();
    }

    public void PlayDeathAudio()
    {
        deathAudio.Play();
    }
}
