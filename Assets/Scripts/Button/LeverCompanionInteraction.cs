using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverCompanionInteraction : MonoBehaviour
{
    public GameObject companion;

    void Start()
    {
        //companion = GameObject.FindWithTag("Companion");
    }

    public void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Companion")
        {
            //companion.SetActive(false);
            companion.GetComponent<MeshRenderer>().enabled = false;
        }
        
    }

    public void OnTriggerExit2D(Collider2D target)
    {
        if(target.tag == "Companion")
        {
            companion.SetActive(true);
        }
    }
}
