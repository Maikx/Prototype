using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCutscene : MonoBehaviour
{
    public Animator fadeInOutAnimator;

    public void OnTriggerEnter2D(Collider2D target)
    {
        fadeInOutAnimator.SetTrigger("Fade Out");

        StartCoroutine(WaitForSec());
    }

    public IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("CutsceneFinale");
    }
}
