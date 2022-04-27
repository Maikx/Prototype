using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
    public int levelToLoad;

    public void LoadNextScene(int levelIndex)
    {
        Cursor.visible = false;
        levelIndex = levelToLoad;
        SceneManager.LoadScene(levelToLoad);
    }
}
