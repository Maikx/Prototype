using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnUI : MonoBehaviour
{
    public GameObject UI_Component;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (UI_Component != null)
        UI_Component.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (UI_Component != null)
        Destroy(UI_Component);
    }
}
