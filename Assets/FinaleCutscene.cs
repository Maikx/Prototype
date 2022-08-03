using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinaleCutscene : MonoBehaviour
{
    public AudioSource bark;
    public Animator fadeInOutAnimator;

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayBark()
    {
        bark.Play();
    }

    public void StartFadeOut()
    {
        fadeInOutAnimator.SetTrigger("Fade Out");
    }
}
