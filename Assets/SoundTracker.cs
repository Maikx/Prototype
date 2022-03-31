using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTracker : MonoBehaviour
{
    public AudioSource randomSound;
    public AudioSource[] audioSources;
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            audioSources[0].Play();
        }
    }
    void RandomSoundness()

    {

        randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];

        randomSound.Play();

    }
}
