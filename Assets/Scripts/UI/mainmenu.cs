using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class mainmenu : MonoBehaviour
{
    public GameObject pnlSettings;
    public GameObject pnlCredits;
    public GameObject pnlMainMenu;
    public GameObject pnlAudio;
    public GameObject pnlControls;
    public GameObject pnlVideo;

    public void PlayGame()
    {
        SceneManager.LoadScene("Full Scene Tutorial");
        Cursor.visible = false;
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

    public void LoadTestLightning()
    {
        SceneManager.LoadScene("Test Lighting");
    }
}
 
