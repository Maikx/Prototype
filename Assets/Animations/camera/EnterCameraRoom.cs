using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCameraRoom : MonoBehaviour
{
    [HideInInspector] public Animator player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        player.SetBool("Dolly 1", true);
    }
}
