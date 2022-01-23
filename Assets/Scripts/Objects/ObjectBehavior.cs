using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    [Header("Misc Parameters")]
    public bool isGrounded;
    [HideInInspector] public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Levitation Parameters")]
    public float floatingMaxHeight = 2;
    public float floatStrength = 2;
    public float randomRotationStrength;

    [HideInInspector]public bool isLevitating;
    [HideInInspector]public bool isStopped;

    void Start()
    {
        groundCheck = gameObject.transform.Find("GroundCheck");
    }

    private void Update()
    {
        CheckIfGrounded();
        Levitate();
        Stop();
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

    public void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.80f, groundLayer);
    }
}
