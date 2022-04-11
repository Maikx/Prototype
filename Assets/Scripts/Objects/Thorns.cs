using UnityEngine;
using UnityEngine.SceneManagement;

public class Thorns : MonoBehaviour
{
    private bool trapIsTouched; // -> set private

    private void Start()
    {
        trapIsTouched = false; // -> verificare le collisioni
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trapIsTouched = true;  
           
        }
    }

    public bool OnTouchTrap()
    { 
        if (trapIsTouched)
            return true;
        else return false;
    }

    private void Update()
    {
        if (trapIsTouched)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // l'oggetto (Health Manager non è presente nel prefab: player)
}
