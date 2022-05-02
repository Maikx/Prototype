using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    
    public GameObject companion;
    public GameObject companionSpawnObj;
    public float targetTime = 6.0f;
    private float currentTime;

    [HideInInspector] public PlayerController pC;
    [HideInInspector] public bool playerIsStopped;
    [HideInInspector] public bool companionIsSpawned;

    void Start()
    {
        pC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        currentTime = targetTime;
    }

    void Update()
    {
        TimerDown();
    }

    void TimerDown ()
    {
        if (playerIsStopped == true)
        {
             if(currentTime > 0) currentTime -= Time.deltaTime;
             if (currentTime <= 0.0f) TimerEnded();
        }
    }

    void SpawnCompanion()
    {
        companion.SetActive(true);
        if (companionSpawnObj != null) companion.transform.position = companionSpawnObj.transform.position;
        else companion.transform.position = pC.transform.position;
        companionIsSpawned = true;
    }

    void StopPlayer ()
    {
        pC.CanMove = false;
        playerIsStopped = true;
    }

    void TimerEnded()
    {
        pC.CanMove = true;
        if(!companionIsSpawned) SpawnCompanion();
        playerIsStopped = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 && playerIsStopped == false && companionIsSpawned == false)
        {
            StopPlayer();
        }
    }
}
