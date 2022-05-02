using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cinematic : MonoBehaviour
{
    [HideInInspector] public PlayerController pC;
    public float targetTime = 6.0f;

    public bool stopPlayer = false;

    void Start()
    {
        pC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        TimerDown();
    }

    void TimerDown ()
    {
        if (stopPlayer == true)
        {

             targetTime -= Time.deltaTime;
 
             if (targetTime <= 0.0f)
             {
                 timerEnded();
             } 
        }
    }

    void StopPlayer ()
    {
        pC.CanMove = false;
        stopPlayer = true;
    }

    void timerEnded()
    {
        pC.CanMove = true;
        stopPlayer = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 && stopPlayer == true)
        {
            StopPlayer();
        }
    }
}
