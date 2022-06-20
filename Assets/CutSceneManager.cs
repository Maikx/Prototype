using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour
{
    public int levelToLoad;
    public Animator animatorCutScene, animatorFadeInOut;
    public GameObject[] objectsToShutDown;

    public void FadeToLevel(int levelIndex)
    {
        levelIndex = levelToLoad;
        animatorFadeInOut.SetTrigger("Fade Out");
        SceneManager.LoadScene(levelToLoad);
    }

    public void StartSecondCutscene()
    {        
        animatorCutScene.SetTrigger("StartCutScene2");
    }

    public void ShutDownObjects()
    {
        for (int i = 0; i < objectsToShutDown.Length; i++)
        {
            objectsToShutDown[i].gameObject.SetActive(false);
        }
    }
}
