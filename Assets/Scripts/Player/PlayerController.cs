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
    [HideInInspector] public HealthManager healthManager;

    public bool CanMove { get; private set; } = true;
    private bool isRunning => canRun && Input.GetKey(sprintKey);
    private bool isJumping => canJump && Input.GetKey(jumpKey);

    [Header("Function Options")]
    [SerializeField] private bool canRun = true;
    [SerializeField] private bool canJump = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("Movement Parameters")]
    [HideInInspector] private Vector3 direction;
    [HideInInspector] public float hInput;
    [HideInInspector] public float accelRatePerSec;
    [HideInInspector] public float currentSpeed;
    [HideInInspector] public float jumpTimer = 0;
    public float walkSpeed = 8;
    public float grabSpeed = 4;
    public float airborneSpeed = 4;
    public float jumpDelay = 2;
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
        healthManager = GameObject.FindObjectOfType<HealthManager>();
        groundCheck = gameObject.transform.Find("GroundCheck");
    }


    void Update()
    {
        if(CanMove)
        {
            HandleMovementInput();
            PlayerAnimator();
            CheckIfCanDoStuff();
        }

        //test HealthStystem
        if (Input.GetKeyDown(KeyCode.K)) healthManager.OnDamage(1);
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
        if (!isRunning && !oG.isGrabbed && isGrounded)
        {
            currentSpeed = walkSpeed;
            jumpTimer = -1;
        }
        else
        {
            if (isRunning && canRun)
            {
                currentSpeed += accelRatePerSec * Time.deltaTime;
                currentSpeed = Mathf.Min(currentSpeed, runSpeed);
            }
            if (oG.isGrabbed)
            {
                currentSpeed = grabSpeed;
            }
            if (!isGrounded)
            {
                currentSpeed = airborneSpeed;
            }
        }

        if (jumpTimer > 0)
        {
            jumpTimer += -Time.deltaTime;
        }
        else
        {
            if (isJumping)
            {
                jumpTimer = jumpDelay;
                rB.velocity = new Vector2(rB.velocity.x, jumpForce);
            }
        }

        rB.velocity = new Vector2(direction.x, rB.velocity.y);
    }

    void CheckIfCanDoStuff()
    {
        if (isGrounded && !oG.isGrabbed && !isJumping) canRun = true;
        else canRun = false;

        if (jumpTimer < 0 && !oG.isGrabbed) canJump = true;
        else canJump = false;
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
