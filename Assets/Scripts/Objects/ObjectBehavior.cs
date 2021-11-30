using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    public bool isGrounded;

    [Header("Misc Parameters")]
    public LayerMask groundLayer;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 
            && !isGrounded)
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8
            && isGrounded)
        {
            isGrounded = false;
        }
    }
}
