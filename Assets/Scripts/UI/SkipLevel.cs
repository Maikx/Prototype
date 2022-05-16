using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipLevel : MonoBehaviour
{
    public Transform checkpoint;
    public GameObject player;
    public KeyCode key;

    public pauseManager pauseScript;

    public void Start()
    {
        pauseScript = pauseScript.GetComponent<pauseManager>();
    }
    //public void SkipToPosition()
    //{
        //GameManager.instance.RestartPlayerPosition = checkpoint.position;
        //SceneManager.LoadScene("Full Scene Tutorial");
        //player.transform.position = checkpoint.position;
    //}
    void Update()
    {
        if (Input.GetKeyDown(key))
            SkipToLevelX();
    }

    public void SkipToLevelX()
    {
        GameManager.instance.RestartPlayerPosition = checkpoint.position;
        SceneManager.LoadScene("Full Scene Tutorial");
        pauseScript.isPause = false;
        Time.timeScale = 1;
        //player.transform.position = checkpoint.position;
    }
}
