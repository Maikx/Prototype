using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class mainmenu : MonoBehaviour
{
    public GameObject pnlSettings;
    public GameObject pnlCredits;
    public GameObject pnlMainMenu;

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
 
