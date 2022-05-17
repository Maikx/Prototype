using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCameraRoom : MonoBehaviour
{
    [HideInInspector] public Animator player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    public void OnTriggerExit2D(Collider2D target)
    {
        if(target.tag == "Player")
        player.SetBool("Dolly 1", false);
    }
}
