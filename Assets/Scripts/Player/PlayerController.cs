using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ObjectGrab))]
[RequireComponent(typeof(BarkInteraction))]

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public ObjectGrab oG;
    [HideInInspector] public BarkInteraction bI;
    [HideInInspector] public Rigidbody2D rB;
    private SoundTracker sT;

    [HideInInspector] public Animator stateMachine;
    public Animator animHandler;
    private enum FacingDirectionHorizontal { Left, Right }
    private enum FacingDirectionVertical { Up, Down, None }
    [HideInInspector] public HealthManager healthManager;
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public CheckPointManager checkPointManager;

    public bool CanMove { get; set; } = true;
    public bool IsMoving { get; private set; }
    private bool isJumping => canJump && Input.GetKey(jumpKey);
    public bool isBarking => canBark && Input.GetKey(barkKey);

    [Header("Function Options")]
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canBark = true;
    [SerializeField] [HideInInspector] FacingDirectionHorizontal currentDirectionHorintal = FacingDirectionHorizontal.Right;
    [SerializeField] [HideInInspector] FacingDirectionVertical currentDirectionVertical = FacingDirectionVertical.None;

    [Header("Controls")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode barkKey = KeyCode.B;

    [Header("Movement Parameters")]
    public float walkSpeed;
    public float grabSpeed;
    public float airborneSpeed;
    public float jumpForce;
    public float grounCheckSize;
    public float timeAfterJumpAgain;
    private float currentTime;
    private float currentSpeed;
    private Vector3 direction;
    [HideInInspector] public float hInput;
    [HideInInspector] public float vInput;

    [Header("Physics Parameters")]
    public float acceleration;
    public float friction;
    public float gravity;

    [Header("Misc Parameters")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Transform groundCheckBack;
    private bool animIsJumping;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool hasGroundBehind;
    

    public Thorns thorns;
    public GameObject transparentObject;
    public Vector3 transparentObjectSize;


    void Awake()
    {
        rB = gameObject.GetComponent<Rigidbody2D>();
        oG = gameObject.GetComponent<ObjectGrab>();
        bI = gameObject.GetComponent<BarkInteraction>();
        stateMachine = gameObject.GetComponent<Animator>();
        sT = gameObject.GetComponent<SoundTracker>();
        Physics.SyncTransforms();
    }

    void Start()
    {
        healthManager = GameObject.FindObjectOfType<HealthManager>();
        checkPointManager = GameObject.FindObjectOfType<CheckPointManager>();
        thorns = GameObject.FindObjectOfType<Thorns>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        transform.position = GameManager.instance.RestartPlayerPosition; // <- delete this once tested
    }

    private void FixedUpdate()
    {
        HandleMovementInput();
        //This is the sphere that checks if the player is grounded.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, grounCheckSize, groundLayer);

        hasGroundBehind = Physics2D.OverlapCircle(groundCheckBack.position, 0.15f, groundLayer);

        //This HandlesPlayerMovement
        rB.velocity += new Vector2(direction.x * acceleration * Time.deltaTime, 0);
        rB.velocity -= new Vector2(rB.velocity.x * friction * Time.deltaTime, gravity * Time.deltaTime);
        //This plays when the player jumps
        if (Input.GetKey(jumpKey) && isGrounded && !oG.isGrabbed && canJump)
        {
            rB.velocity = new Vector2(rB.velocity.x, jumpForce);
            animIsJumping = true;
            currentTime = timeAfterJumpAgain;
        }
    }

    void Update()
    {
            PlayerStateMachine();
            PlayerAnimationHandler();
            CheckPlayerLookingDirection();

        CheckIfMovingBackGrabbed();
        JumpTimer();

        if (Input.GetKeyDown(KeyCode.L)) // <- only for testing!
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (thorns != null && thorns.OnTouchTrap() == true) // <- works
        {
            GameManager.instance.SetHealth(0);

            if(GameManager.instance.health == 0)
            {
               SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
           
        }
    }

    void TransparentObjcetResizing()
    {
        if (transparentObject != null)
            transparentObject.gameObject.transform.localScale = transparentObjectSize;
    }

    /// <summary>
    /// This handles the player overall movements and some of states..
    /// </summary>
    void HandleMovementInput()
    {
        //This makes the player move with horizontal inputs (A/D & arrows).
        if (CanMove)
        {
            hInput = Input.GetAxis("Horizontal");
            vInput = Input.GetAxis("Vertical");
        }
        else
        {
            hInput = 0;
            vInput = 0;
        }

        direction.x = hInput * (currentSpeed);

        //This is for the player's facing direction!
        if (hInput != 0 && !oG.isGrabbed) transform.right = direction;

        //This changes player speed depending on the state ex: walk/grab/airborne
        if (!oG.isGrabbed && isGrounded)
        {
            currentSpeed = walkSpeed;
        }
        else
        {
            if (oG.isGrabbed)
            {
                currentSpeed = grabSpeed;
            }
            if (!isGrounded)
            {
                currentSpeed = airborneSpeed;
            }
        }

        //This Handles the bark function
        if (Input.GetKey(barkKey) && isGrounded && !oG.isGrabbed && canBark)
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

            sT.BarkSound();
        }
    }

    /// <summary>
    /// This is used to memorize the player facing direction for the bark
    /// </summary>
    public void CheckPlayerLookingDirection()
    {
        if (hInput > 0 && oG.isGrabbed == false)
            currentDirectionHorintal = FacingDirectionHorizontal.Right;

        else if (hInput < 0 && oG.isGrabbed == false)
            currentDirectionHorintal = FacingDirectionHorizontal.Left;

        else if (vInput > 0 && oG.isGrabbed == false)
            currentDirectionVertical = FacingDirectionVertical.Up;
        else if (vInput < 0 && oG.isGrabbed == false)
            currentDirectionVertical = FacingDirectionVertical.Down;
        else if (vInput == 0 && oG.isGrabbed == false)
            currentDirectionVertical = FacingDirectionVertical.None;
    }

    /// <summary>
    /// This is used to memorize if the player moving backwards in grabbed state
    /// </summary>
    void CheckIfMovingBackGrabbed()
    {
        if (oG.isGrabbed && currentDirectionHorintal == FacingDirectionHorizontal.Right && hInput < 0 || oG.isGrabbed && currentDirectionHorintal == FacingDirectionHorizontal.Left && hInput > 0)
            oG.isMovingBackGrabbed = true;
        else if(oG.isGrabbed && currentDirectionHorintal == FacingDirectionHorizontal.Right && hInput >= 0 || oG.isGrabbed && currentDirectionHorintal == FacingDirectionHorizontal.Left && hInput <= 0)
            oG.isMovingBackGrabbed = false;
    }

    void JumpTimer()
    {
        if (currentTime > 0)
        {
            canJump = false;
            animIsJumping = false;
            currentTime -= Time.deltaTime;
        }
        else if (currentTime <= 0)
        {
            canJump = true;
        }
    }

    /// <summary>
    /// This manages the player's movement animator, not the animations!
    /// </summary>
    public void PlayerStateMachine()
    {
        if (hInput != 0) stateMachine.SetBool("isMoving", true);
        else stateMachine.SetBool("isMoving", false);
        stateMachine.SetBool("isGrounded", isGrounded);
        stateMachine.SetBool("isGrabbed", oG.isGrabbed);
    }

    public void PlayerAnimationHandler()
    {
        if (hInput != 0) animHandler.SetBool("isMoving", true);
        else animHandler.SetBool("isMoving", false);
        animHandler.SetBool("isGrounded", isGrounded);
        animHandler.SetBool("isGrabbed", oG.isGrabbed);
        animHandler.SetBool("isBarking", isBarking);
        animHandler.SetInteger("BarkDirection", bI.lastDirection);
        animHandler.SetBool("isMovingBackGrabbed", oG.isMovingBackGrabbed);
        if (animIsJumping) animHandler.SetTrigger("isJumping");
    }
}
