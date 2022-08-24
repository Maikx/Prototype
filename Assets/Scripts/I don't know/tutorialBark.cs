using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialBark : MonoBehaviour
{
    public GameObject nextTrigger;
    
    void Start()
    {
        nextTrigger.SetActive(false);
    }

    public void OnTriggerExit2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            nextTrigger.SetActive(true);
        }
    }
}
