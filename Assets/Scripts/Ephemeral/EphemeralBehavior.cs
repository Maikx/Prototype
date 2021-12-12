using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EphemeralBehavior : MonoBehaviour
{
    [HideInInspector] public Animator ephemeralController;
    public int moveAmount;
    public int movementSpeed;

    private void Start()
    {
        ephemeralController = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bark")
        {
            Debug.Log("I'm running!");
            ephemeralController.SetTrigger("Move");
        }
    }

}
