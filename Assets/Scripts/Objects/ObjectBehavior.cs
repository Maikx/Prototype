using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    [Header("Misc Parameters")]
    public bool useGravity = true;
    public enum ObjectType { Default, Box, Rope}
    public ObjectType objectType;

    [Header("Boulders Parameters")]
    public bool isTrap;
    public float floatingMaxHeight = 2;
    public float floatStrength = 2;
    public float randomRotationStrength;

    [Header("Grounded Parameters")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckSize = 0.6f;
    public bool isGrounded;

    [HideInInspector]public bool isLevitating;
    [HideInInspector]public bool isStopped;

    private void Start()
    {
        if (isTrap) gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    private void FixedUpdate()
    {
        if(useGravity) GroundCheck();
    }

    private void Update()
    {
        Levitate();
        Stop();
    }

    public void Activate()
    {
        if (isTrap) gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        else if (objectType == ObjectType.Rope) Rope();
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

    void Rope()
    {
        gameObject.GetComponent<RopeBehavior>().MakePlanksFall();
    }

    void Stop()
    {
        if(isStopped && gameObject.GetComponent<Rigidbody2D>() != null)
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        else if (!isStopped && gameObject.GetComponent<Rigidbody2D>() != null)
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckSize, groundLayer);
    }
}
