using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ObjectGrab))]
[RequireComponent(typeof(BarkInteraction))]

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public ObjectGrab oG;
    [HideInInspector] public BarkInteraction bI;
    [HideInInspector] public Rigidbody2D rB;
    [HideInInspector] public Animator anim;
    private enum FacingDirectionHorizontal { Left, Right }
    private enum FacingDirectionVertical { Up, Down, None }

    public bool CanMove { get; private set; } = true;
    public bool IsMoving { get; private set; }
    private bool isRunning => canRun && Input.GetKey(sprintKey);
    private bool isJumping => canJump && Input.GetKey(jumpKey);
    private bool isBarking => canBark && Input.GetKey(barkKey);

    [Header("Function Options")]
    [SerializeField] private bool canRun = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canBark = true;
    //bool wasPreviouslyMoving;
    [SerializeField] FacingDirectionHorizontal currentDirectionHorintal = FacingDirectionHorizontal.Right;
    [SerializeField] /*[HideInInspector]*/ FacingDirectionVertical currentDirectionVertical = FacingDirectionVertical.None;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode barkKey = KeyCode.B;

    [Header("Movement Parameters")]
    [HideInInspector] private Vector3 direction;
    /*[HideInInspector]*/ public float hInput;
    /*[HideInInspector]*/ public float vInput;
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
        rB = gameObject.GetComponent<Rigidbody2D>();
        oG = gameObject.GetComponent<ObjectGrab>();
        bI = gameObject.GetComponent<BarkInteraction>();
        anim = gameObject.GetComponent<Animator>();
        groundCheck = gameObject.transform.Find("GroundCheck");
    }

    private void Start()
    {
        //This is used to gain speed overtime
        accelRatePerSec = runSpeed / timeZeroToMax;
    }

    void Update()
    {

        if (CanMove)
        {
            HandleMovementInput();
            PlayerAnimator();
            CheckIfCanDoStuff();
        }
        CheckPlayerLookingDirection();
    }

    /// <summary>
    /// This handles the player overall movements!
    /// </summary>
    void HandleMovementInput()
    {
        //This makes the player move with horizontal inputs (A/D & arrows).
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
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

        if (Input.GetKey(barkKey))
        {
            switch (currentDirectionVertical)
            {
                case FacingDirectionVertical.Up:
                    bI.BarkUp();
                    break;
                case FacingDirectionVertical.Down:
                    bI.BarkDown();
                    break;
                case FacingDirectionVertical.None:
                    if (currentDirectionHorintal == FacingDirectionHorizontal.Right)
                        bI.BarkRight();
                    else if (currentDirectionHorintal == FacingDirectionHorizontal.Left)
                        bI.BarkLeft();
                    break;
            }
        }
    }

    void CheckIfCanDoStuff()
    {
        if (isGrounded && !oG.isGrabbed && !isJumping) canRun = true;
        else canRun = false;

        if (jumpTimer < 0 && !oG.isGrabbed) canJump = true;
        else canJump = false;
    }

    public void CheckPlayerLookingDirection()
    {
        if (hInput > 0)
            currentDirectionHorintal = FacingDirectionHorizontal.Right;

        else if (hInput < 0)
            currentDirectionHorintal = FacingDirectionHorizontal.Left;

        else if (vInput > 0)
            currentDirectionVertical = FacingDirectionVertical.Up;
        else if (vInput < 0)
            currentDirectionVertical = FacingDirectionVertical.Down;
        else if (vInput == 0)
            currentDirectionVertical = FacingDirectionVertical.None;
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
