using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LightUpButtom : MonoBehaviour
{
    public GameObject LightUpObject;

    void OnMouseOver()
    {
        LightUpObject.SetActive(true);
    }

    void OnMouseExit()
    {
        LightUpObject.SetActive(false);
    }
}
