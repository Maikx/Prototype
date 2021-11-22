using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public bool CanMove { get; private set; } = true;
    public bool grabZone = false; 
    private bool isRunning => canRun && Input.GetKey(sprintKey);
    [HideInInspector]
    public CharacterController controller;
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
        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                direction.y = jumpForce;
            }
            canRun = true;
        }
        else
        {
            canRun = false;
        }
        controller.Move(direction * Time.deltaTime);
        direction.y += gravity * Time.deltaTime;
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
