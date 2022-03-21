using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppareUI : MonoBehaviour
{
    public GameObject UI_Component;

    void OnTriggerStay2D(Collider2D other)
    {
        if (UI_Component != null)
        UI_Component.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (UI_Component != null)
        {
            UI_Component.SetActive(false);
            Destroy(UI_Component);
        }
    }    
}
