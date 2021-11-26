using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public bool CanMove { get; private set; } = true;
    public bool grabZone = false; 
    private bool isRunning => canRun && Input.GetKey(sprintKey);
    public Rigidbody2D rb;
    [HideInInspector]
    private Vector3 direction;

    [Header("Function Options")]
    [SerializeField] private bool canRun = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Movement Parameters")]
    public float walkSpeed = 8;
    public float runSpeed = 20;
    public float jumpForce = 10;
    public float gravity = -20;

    [Header("Misc Parameters")]
    public Transform groundCheck;
    public LayerMask groundLayer;

    private void Start()
    {
        rb.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(CanMove)
        {
            HandleMovementInput();
        }
    }

    private void HandleMovementInput()
    {
        float hInput = Input.GetAxis("Horizontal");
        direction.x = hInput * (isRunning ? runSpeed : walkSpeed);
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            canRun = true;
        }
        else
        {
            canRun = false;
        }
        rb.velocity = new Vector2(direction.x, rb.velocity.y);
        //direction.y += gravity * Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "Grab_Zone")
        {
            grabZone = true;
            anim.SetBool("GrabZone", true);
            Debug.Log("SOLO ZOOM TEST!!!!!!!!!!!");
            Debug.Log(gameObject.name);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Grab_Zone")
        {
            grabZone = false;
            anim.SetBool("GrabZone", false);
        }
    }
}
