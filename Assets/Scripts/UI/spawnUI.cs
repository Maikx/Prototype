using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnUI : MonoBehaviour
{
    public GameObject UI_Component;
    public KeyCode keyPressed = KeyCode.E;
    private bool UIActivated;

    void Start()
    {
        UI_Component.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player" && UI_Component != null)
        UI_Component.SetActive(true);
        UIActivated = true;
    }

    //void OnTriggerExit2D(Collider2D target)
    //{
        //if (target.tag == "Player" && UI_Component != null)
        //Destroy(UI_Component);
    //}

    void OnTriggerStay2D(Collider2D target)
    {
        if (UI_Component != null && Input.GetKeyDown(keyPressed) && UIActivated == true && target.tag == "Player")
            Destroy(UI_Component);
    }
}
