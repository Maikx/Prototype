using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMouse : MonoBehaviour
{
    public GameObject mousen;
    public GameObject mouse1;
    public GameObject mouse2;

    public void MouseOff()
    {
        mousen.SetActive(true);
        mouse1.SetActive(false);
        mouse2.SetActive(false);
    }

    public void MouseOn()
    {
        mousen.SetActive(false);
        mouse1.SetActive(true);
        mouse2.SetActive(true);
    }
}
