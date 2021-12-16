using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public HealthManager healthManager;
    public PlayerController playerController;
    public CheckPointManager CheckPointManager;
    public Vector3 PlayerRestartPos;


    private void Awake()
    {
       
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void Respawn()  //// <--- move the method here once tested
    {
        /*
        if (!healthManager.playerIsLive)
        {
            CheckPointManager.LastCheckpointActive();
        }
        */
    }

    public void Update()
    {
        //Respawn();
    }

}
