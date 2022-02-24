using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class mainmenu : MonoBehaviour
{
    public GameObject pnlSettings;
    public GameObject pnlCredits;
    public GameObject pnlMainMenu;
    public GameObject pnlPause;
    public bool isPause = false;

    public void Update ()
    {
        if(Input.GetKeyDown("escape") && isPause == false)
        {
            pnlPause.SetActive(true);
            Time.timeScale = 0;
            isPause = true;
        }
        else if (Input.GetKeyDown("escape") && isPause == true)
        {
            pnlPause.SetActive(false);
            Time.timeScale = 1;
            isPause = false;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Full Scene Tutorial");
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

    public void OpenSetting()
    {
        pnlMainMenu.SetActive(false);
        pnlSettings.SetActive(true);
    }

    public void OpenCredits()
    {
        pnlMainMenu.SetActive(false);
        pnlCredits.SetActive(true);
    }

    public void CloseSetting()
    {
        pnlMainMenu.SetActive(true);
        pnlSettings.SetActive(false);
    }

    public void CloseCredits()
    {
        pnlMainMenu.SetActive(true);
        pnlCredits.SetActive(false);
    }
}
 
