using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Script in testing, don’t touch the values!!!!!

    public bool isActive = false;
    public float timeToDisable = 1.0f;
    public float timer;

    // ref
    CheckPointManager checkPointManager;
    GameManager gameManager;

    private void Start()
    {
        checkPointManager = GameObject.FindObjectOfType<CheckPointManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActive = true;
            //checkPointManager.AddPositionGameObjectToList(this.transform); // registra la posizione
            GameManager.instance.RestartPlayerPosition = transform.position;
        }
    }

    public void Update()
    {
        /*
        if (isActive)
        {
            timer += Time.deltaTime;
            if(timer > timeToDisable)
            {
                isActive = false;
            }
        }
        */
    }
}
