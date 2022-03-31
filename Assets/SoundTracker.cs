using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTracker : MonoBehaviour
{
    public AudioSource randomSound;
    public AudioClip[] audioSources;
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            RandomSoundness();
        }
    }
    void RandomSoundness()

    {

        randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];

        randomSound.Play();

    }
}
