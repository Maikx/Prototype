using UnityEngine;

/// <summary>
/// contains functions needed to set the health of the player
/// </summary>
public class HealthManager : MonoBehaviour
{

    public int health = 1;
    public int minHealth = 0;
    public bool playerIsLive = true;

    private void Start()
    {
        health = 2;
        playerIsLive = true;
    }

    /// <summary>
    /// call where the player takes damage
    /// </summary>
    /// <param name="damage"></param>
    public void OnDamage(int damage) // <- set with the correct values
    {
        health -= damage;

        if(health <= minHealth)
        {
            health = minHealth;
            playerIsLive = false;
        }
    }

}
