using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsFullscreen : MonoBehaviour
{
    public GameObject yes;
    public GameObject no;

    private void Start()
    {
        if (Screen.fullScreen == true)
        {
            yes.SetActive(true);
            no.SetActive(false);
        }
        else
        {
            no.SetActive(true);
            yes.SetActive(false);
        }
    }

    public void SwitchingMode()
    {
        if(Screen.fullScreen == true)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            yes.SetActive(false);
            no.SetActive(true);
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            no.SetActive(false);
            yes.SetActive(true);
        }
    }
}
