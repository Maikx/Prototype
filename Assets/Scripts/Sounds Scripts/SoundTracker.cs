using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTracker : MonoBehaviour
{
    public AudioSource randomSound;
    public AudioClip[] audioSources;

    public void BarkSound()
    {
            randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];
            randomSound.Play();
    }
}
