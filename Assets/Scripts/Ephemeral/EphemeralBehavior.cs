using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EphemeralBehavior : MonoBehaviour
{
    [HideInInspector] public Animator ephemeralController;
    
    [Header("Function Options")]
    [SerializeField] public bool canMove;

    [Header("Movement Parameters")]
    public int hDistance;
    public int vDistance;
    public int hSpeed;
    public int vSpeed;

    private void Awake()
    {
        ephemeralController = gameObject.GetComponent<Animator>();
        ephemeralController.SetBool("CanMove", canMove);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bark" && ephemeralController.GetBool("CanMove") == true)
        {
            //0 = none, 1 = right, 2 = left, 3 = up, 4 = down..
            if (collision.GetComponentInParent<BarkInteraction>().lastDirection == 1)
            {
                ephemeralController.SetInteger("Direction", 1);
                ephemeralController.SetTrigger("Move");
            }
            else if (collision.GetComponentInParent<BarkInteraction>().lastDirection == 2)
            {
                ephemeralController.SetInteger("Direction", 2);
                ephemeralController.SetTrigger("Move");
            }
            else if (collision.GetComponentInParent<BarkInteraction>().lastDirection == 3)
            {
                ephemeralController.SetInteger("Direction", 3);
                ephemeralController.SetTrigger("Move");
            }
            else if (collision.GetComponentInParent<BarkInteraction>().lastDirection == 4)
            {
                ephemeralController.SetInteger("Direction", 4);
                ephemeralController.SetTrigger("Move");
            }
        }
        else if(collision.tag == "Totem")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
