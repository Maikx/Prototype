using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
    public int levelToLoad;

    void Start()
    {
        StartCoroutine(ForcedPlay());
    }

    public void LoadNextScene(int levelIndex)
    {
        Cursor.visible = false;
        levelIndex = levelToLoad;
        SceneManager.LoadScene(levelToLoad);
    }

    IEnumerator ForcedPlay()
    {
        yield return new WaitForSeconds(24.0f);
        LoadNextScene(levelToLoad);
    }
}
