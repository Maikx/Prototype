using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cinematic : MonoBehaviour
{
    public PlayerController pC;
    public float targetTime = 6.0f;
    public bool stopPlayer;

    void Start()
    {
        pC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

    }

    void StopPlayer ()
    {
        pC.hInput = 0;
        pC.vInput = 0;
        pC.CanMove = false;
        stopPlayer = false;
    }

    void timerEnded()
    {
        pC.CanMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 && stopPlayer == true)
        {
            StopPlayer();
        }
    }
}
