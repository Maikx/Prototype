using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCameraRoom : MonoBehaviour
{
    [HideInInspector] public Animator player;
    public GameObject cameraOff;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    public void OnTriggerExit2D(Collider2D target)
    {
        if(target.tag == "Player")
        cameraOff.SetActive(false);
    }
}
