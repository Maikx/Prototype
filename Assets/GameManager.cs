using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //public HealthManager healthManager;  // <- delete once tested
    public PlayerController playerController;
    public CheckPointManager CheckpointManager;

    public Vector2 RestartPlayerPosition;

    public int health;


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
    private void Start()
    {
        CheckpointManager = GameObject.FindObjectOfType<CheckPointManager>();
        health = 1;
    }
    public void RespawnPlayer() // <- add this 
    {  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public int SetHealth(int value)
    {
        return health = value;
    }

    public void Update()
    {

    }

}
