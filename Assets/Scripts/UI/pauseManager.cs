using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pnlPause;
    public GameObject pnlSettings;
    public GameObject pnlAudio;
    public GameObject pnlControls;
    public GameObject pnlVideo;

    public AudioSource MenuIn, MenuOut;
    //public GameObject skipLevel;

    public bool isPause = false;

    public void Update()
    {
        GamePause();
    }

    public void GamePause()
    {
        if (Input.GetKeyDown("escape") && isPause == false)
        {
            //skipLevel.SetActive(false);
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            isPause = true;
            Cursor.visible = true;
            MenuIn.Play();
        }
        else if (Input.GetKeyDown("escape") && isPause == true)
        {
            //skipLevel.SetActive(true);
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            isPause = false;
            Cursor.visible = false;
            MenuOut.Play();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
        Cursor.visible = false;
        MenuOut.Play();
    }

    public void OpenSetting()
    {
        pnlPause.SetActive(false);
        pnlSettings.SetActive(true);
    }

    public void openAudioSettings()
    {
        pnlAudio.SetActive(true);
        pnlControls.SetActive(false);
        pnlVideo.SetActive(false);
    }

    public void openVideoSettings()
    {
        pnlAudio.SetActive(false);
        pnlControls.SetActive(false);
        pnlVideo.SetActive(true);
    }

    public void openControlsSettings()
    {
        pnlAudio.SetActive(false);
        pnlControls.SetActive(true);
        pnlVideo.SetActive(false);
    }

    public void returnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void CloseSetting()
    {
        pnlPause.SetActive(true);
        pnlSettings.SetActive(false);
    }
}
