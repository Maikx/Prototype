using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class mainmenu : MonoBehaviour
{
    public string nameOfPlayer;
    public string saveName;

    public TextMeshProUGUI inputText;
    public TextMeshProUGUI loadName;

    [Header("Panels")]
    public GameObject pnlNameSelection;
    public GameObject pnlSettings;
    public GameObject pnlCredits;
    public GameObject pnlMainMenu;
    public GameObject pnlAudio;
    public GameObject pnlControls;
    public GameObject pnlVideo;

    void Awake()
    {
        pnlMainMenu.SetActive(false);
        pnlCredits.SetActive(false);
        pnlSettings.SetActive(false);
        PlayerPrefs.SetString("name", "Nameless");
    }

    void Update()
    {
        nameOfPlayer = PlayerPrefs.GetString("name", "none");
        loadName.text = nameOfPlayer;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("test cutscene iniziale");
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
    
    public void SetName()
    {
        saveName = inputText.text;
        PlayerPrefs.SetString("name", saveName);
        pnlMainMenu.SetActive(true);
        pnlNameSelection.SetActive(false);
    }
}
 
