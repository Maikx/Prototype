using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    [Header("Misc Parameters")]
    public bool isGrounded;
    public bool isTrap;

    [Header("Levitation Parameters")]
    public float floatingMaxHeight = 2;
    public float floatStrength = 2;
    public float randomRotationStrength;

    [HideInInspector]public bool isLevitating;
    [HideInInspector]public bool isStopped;

    private void Start()
    {
        if (isTrap) gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    private void Update()
    {
        Levitate();
        Stop();
    }

    public void Activate()
    {
        if (isTrap) gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    void Levitate()
    {
        if (isLevitating)
        {
            if (transform.position.y < floatingMaxHeight)
            {
                transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * floatStrength);
                transform.Rotate(randomRotationStrength, randomRotationStrength, randomRotationStrength);
            }
            if (transform.position.y >= floatingMaxHeight)
            {
                transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * floatStrength / 2);
                transform.Rotate(randomRotationStrength, randomRotationStrength, randomRotationStrength);
            }
        }
    }

    void Stop()
    {
        if(isStopped)
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        else
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isGrounded = false;
        }
    }
}
