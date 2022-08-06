using UnityEngine;
using UnityEngine.SceneManagement;

public class Thorns : MonoBehaviour
{

    private bool trapIsTouched;

    private void Start()
    {
        trapIsTouched = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trapIsTouched = true;  
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trapIsTouched = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trapIsTouched = false;
        }
    }

    public bool OnTouchTrap()
    { 
        if (trapIsTouched)
            return true;
        else return false;
    }
}
