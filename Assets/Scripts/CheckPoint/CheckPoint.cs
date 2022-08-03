using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool isActive = false;
    public float timeToDisable = 1.0f;
    public float timer;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActive = true;
            GameManager.instance.RestartPlayerPosition = transform.position;
        }
    }
}
