using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ObjectGrab))]

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public ObjectGrab oG;
    [HideInInspector] public Rigidbody2D rB;
    [HideInInspector] public Animator anim;

    public bool CanMove { get; private set; } = true;
    private bool isRunning => canRun && Input.GetKey(sprintKey);

    [Header("Function Options")]
    [SerializeField] private bool canRun = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Movement Parameters")]
    [HideInInspector] private Vector3 direction;
    [HideInInspector] public float hInput;
    [HideInInspector] public float accelRatePerSec;
    [HideInInspector] public float currentSpeed;
    public float walkSpeed = 8;
    public float runSpeed = 20;
    public float timeZeroToMax = 2.5f;
    public float jumpForce = 10;
    public float gravity = -20;

    [Header("Misc Parameters")]
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public Transform groundCheck;
    public LayerMask groundLayer;

    void Awake()
    {
        //This is used to gain speed overtime
        accelRatePerSec = runSpeed / timeZeroToMax;
    }


    void Start()
    {
        rB = gameObject.GetComponent<Rigidbody2D>();
        oG = gameObject.GetComponent<ObjectGrab>();
        anim = gameObject.GetComponent<Animator>();
        groundCheck = gameObject.transform.Find("GroundCheck");
    }

    void Update()
    {
        if(CanMove)
        {
            HandleMovementInput();
            PlayerAnimator();
        }
    }

    /// <summary>
    /// This handles the player overall movements!
    /// </summary>
    void HandleMovementInput()
    {
        //This makes the player move with horizontal inputs (A/D & arrows).
        hInput = Input.GetAxis("Horizontal");
        direction.x = hInput * (currentSpeed);
        //This is the sphere that checks if the player is grounded.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);

        //This is for the player's facing direction!
        if (hInput != 0 && !oG.isGrabbed) transform.right = direction;

        //This changes player speed to walking speed if not running.
        if (!isRunning) currentSpeed = walkSpeed;
        else
        {
            currentSpeed += accelRatePerSec * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, runSpeed);
        }

        //This checks if the player can jump.
        if (isGrounded && !oG.isGrabbed)
        {
            if (Input.GetButtonDown("Jump")) rB.velocity = new Vector2(rB.velocity.x, jumpForce);
            canRun = true;
        }
        else canRun = false;

        rB.velocity = new Vector2(direction.x, rB.velocity.y);
    }

    /// <summary>
    /// This manages the player's movement animator, not the animations!
    /// </summary>
    public void PlayerAnimator()
    {
        anim.SetInteger("hInput", Mathf.RoundToInt(Input.GetAxis("Horizontal")));
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isGrabbed", oG.isGrabbed);
    }
}
