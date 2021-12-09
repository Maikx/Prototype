using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    // contains functions needed to set the health of the player

    public int health = 1;
    public int minHealth = 0;
    public bool playerIsLive = true;

    private void Start()
    {
        health = 1;
    }

    public void OnDamage(int damage)
    {
        health -= damage;

        if(health <= minHealth)
        {
            health = minHealth;
            playerIsLive = false;
        }
    }

    private void Update()
    {
        if (!playerIsLive)
        {
            SceneManager.LoadScene(0); // <-- in testing
        }
    }


}
