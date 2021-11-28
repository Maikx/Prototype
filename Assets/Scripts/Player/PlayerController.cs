using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public ObjectGrab oG;
    [HideInInspector] public Rigidbody2D rB;

    public Animator anim;
    public bool CanMove { get; private set; } = true;
    public bool isGrounded;
    private bool isRunning => canRun && Input.GetKey(sprintKey);

    [HideInInspector] private Vector3 direction;
    [HideInInspector] public float hInput;

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
        rB = gameObject.GetComponent<Rigidbody2D>();
        oG = gameObject.GetComponent<ObjectGrab>();
    }

    void Update()
    {
        if(CanMove)
        {
            HandleMovementInput();
            PlayerAnimator();
        }
    }

    private void HandleMovementInput()
    {
        hInput = Input.GetAxis("Horizontal");
        direction.x = hInput * (isRunning ? runSpeed : walkSpeed);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);
        if (isGrounded || !oG.isGrabbed)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rB.velocity = new Vector2(rB.velocity.x, jumpForce);
            }
            canRun = true;
        }
        else
        {
            canRun = false;
        }
        rB.velocity = new Vector2(direction.x, rB.velocity.y);
    }

    public void PlayerAnimator()
    {
        anim.SetInteger("hInput", Mathf.RoundToInt(Input.GetAxis("Horizontal")));
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isGrabbed", oG.isGrabbed);
    }
}
