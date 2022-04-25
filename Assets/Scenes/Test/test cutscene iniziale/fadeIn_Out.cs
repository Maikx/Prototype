using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fadeIn_Out : MonoBehaviour
{
    public Animator animator;

    public int levelToLoad;

    //void Update()
    //{
        //if (Input.GetMouseButtonDown(0))
        //{
            //FadeToLevel(levelToLoad);
        //}
    //}

    public void FadeToLevel(int levelIndex)
    {
        levelIndex = levelToLoad;
        animator.SetTrigger("Fade Out");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
