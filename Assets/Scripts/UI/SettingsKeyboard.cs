using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsKeyboard : MonoBehaviour
{
    public GameObject keyboard1;
    public GameObject keyboard2;
    public GameObject keyboard3;
    public GameObject keyboard4;

    public void Keyboard1()
    {
        if(keyboard1.activeSelf == false)
            keyboard1.SetActive(true);
        else
            keyboard1.SetActive(false);
    }

    public void Keyboard2()
    {
        if (keyboard2.activeSelf == false)
            keyboard2.SetActive(true);
        else
            keyboard2.SetActive(false);
    }

    public void Keyboard3()
    {
        if (keyboard3.activeSelf == false)
            keyboard3.SetActive(true);
        else
            keyboard3.SetActive(false);

    }

    public void Keyboard4()
    {
        if (keyboard4.activeSelf == false)
            keyboard4.SetActive(true);
        else
            keyboard4.SetActive(false);
    }
}
