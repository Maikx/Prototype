using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fadeIn_Out : MonoBehaviour
{
    public Animator animator, animCutscene;

    public int levelToLoad;

    public GameObject[] objectToShutDown;

    public GameObject pnlFadeInOut;

    public void StartCutscene()
    {
        animCutscene.SetTrigger("StartCutScene");
        //for (int i = 0; i < objectToShutDown.Length; i++)
        //{
            //objectToShutDown[i].SetActive(false);
        //}
    }

    public void FadeToLevel(int levelIndex)
    {
        pnlFadeInOut.SetActive(true);
        levelIndex = levelToLoad;
        animator.SetTrigger("Fade Out");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void ShutDownPanel()
    {
        pnlFadeInOut.SetActive(false);
    }

    public void StartFadeOut()
    {
        animator.SetTrigger("Fade Out");
    }
}
