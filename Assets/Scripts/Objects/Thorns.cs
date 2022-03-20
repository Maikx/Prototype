using UnityEngine;

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

    public bool OnTouchTrap()
    {
        if (trapIsTouched)
            return true;
        else return false;
    }

    // l'oggetto (Health Manager non è presente nel prefab: player)



}
