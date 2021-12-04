using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    [Header("Misc Parameters")]
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public Transform groundCheck;
    public LayerMask groundLayer;

    void Start()
    {
        groundCheck = gameObject.transform.Find("GroundCheck");
    }

    private void Update()
    {
        CheckIfGrounded();
    }

    public void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.80f, groundLayer);
    }
}
