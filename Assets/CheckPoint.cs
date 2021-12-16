using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool isActive = false;
    public float timeToDisable = 1.0f;
    public float timer;

    CheckPointManager checkPointManager;
    private GameManager gm;

    private void Start()
    {
        checkPointManager = GameObject.FindObjectOfType<CheckPointManager>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActive = true;

            checkPointManager.AddPositionGameObjectToList(this.transform);

            gm.PlayerRestartPos = checkPointManager.RecordingPosition(this.transform.position);
        }
    }

    public void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if(timer > timeToDisable)
            {
                isActive = false;
            }
        }
    }
}
