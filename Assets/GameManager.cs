using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Vector2 RestartPlayerPosition;

    public int health;
    public float timeAfterRestart;
    [HideInInspector] public float currentTimeAfterRestart;


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
        currentTimeAfterRestart = timeAfterRestart;
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
        if (health == 0)
        {
            if (currentTimeAfterRestart > 0)
            {
                currentTimeAfterRestart -= Time.deltaTime;
            }
            else if (currentTimeAfterRestart <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                instance.SetHealth(1);
                currentTimeAfterRestart = timeAfterRestart;
            }
        }
    }

public int SetHealth(int value)
    {
        return health = value;
    }
}
