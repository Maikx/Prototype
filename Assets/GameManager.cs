using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController playerController;
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
        health = 1;
    }

    private void Update()
    {
        RespawnPlayer();
    }

    /// <summary>
    /// Checks if health is below 1, restarts the scene
    /// </summary>
    public void RespawnPlayer()
    {
        //if (health == 0)
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

public int SetHealth(int value)
    {
        return health = value;
    }
}
