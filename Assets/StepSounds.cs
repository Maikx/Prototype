using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSounds : MonoBehaviour
{
    [SerializeField] private AudioClip DogFootStep1;
    [SerializeField] private AudioClip DogFootStep2;
    [SerializeField] private AudioClip DogFootStep3;
    [SerializeField] private AudioClip DogFootStep4;

    [SerializeField] private AudioClip[] DogFootSepArray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomDogFootStepsFunctions()
    {
        int randomType = UnityEngine.Random.Range(0, 3);
        AudioSource.PlayClipAtPoint(DogFootSepArray[randomType], transform.position);
    }
    public void dogfootstepfunction1()
    {
        AudioSource.PlayClipAtPoint(DogFootStep1, transform.position);
    }
    public void dogfootstepfunction2()
    {
        AudioSource.PlayClipAtPoint(DogFootStep2, transform.position);
    }
    public void dogfootstepfunction3()
    {
        AudioSource.PlayClipAtPoint(DogFootStep3, transform.position);
    }
    public void dogfootstepfunction4()
    {
        AudioSource.PlayClipAtPoint(DogFootStep4, transform.position);
    }
}
