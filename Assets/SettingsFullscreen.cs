using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsFullscreen : MonoBehaviour
{
    public void SwitchingMode()
    {
        //Toggle fullscreen
        Screen.fullScreen = !Screen.fullScreen;
    }
}
