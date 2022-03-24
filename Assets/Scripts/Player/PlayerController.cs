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
    private bool isBarking => canBark && Input.GetKey(barkKey);

    [Header("Function Options")]
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canBark = true;
    [SerializeField] FacingDirectionHorizontal currentDirectionHorintal = FacingDirectionHorizontal.Right;
    [SerializeField] [HideInInspector] FacingDirectionVertical currentDirectionVertical = FacingDirectionVertical.None;

    [Header("Controls")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode barkKey = KeyCode.B;

    [Header("Movement Parameters")]
    public float walkSpeed = 8;
    public float grabSpeed = 4;
    public float airborneSpeed = 4;
    public float jumpForce = 10;
    [HideInInspector] private Vector3 direction;
    [HideInInspector] public float hInput;
    [HideInInspector] public float vInput;
    [HideInInspector] public float currentSpeed;
    [HideInInspector] public float jumpTimer = 0;

    [Header("Misc Parameters")]
    public LayerMask groundLayer;
    public GameObject companionPrefab;
    public Transform groundCheck;
    [HideInInspector] public bool isGrounded;

    public Thorns thorns;
    public GameObject transparentObject;
    public Vector3 transparentObjectSize;


    void Awake()
    {
        rB = gameObject.GetComponent<Rigidbody2D>();
        oG = gameObject.GetComponent<ObjectGrab>();
        bI = gameObject.GetComponent<BarkInteraction>();
        stateMachine = gameObject.GetComponent<Animator>();
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
        //This is the sphere that checks if the player is grounded.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);

        //This HandlesPlayerMovement
        rB.velocity = new Vector2(direction.x, rB.velocity.y);

        //This plays when the player jumps
        if (Input.GetKey(jumpKey) && isGrounded && !oG.isGrabbed && canJump)
        {
            rB.velocity = new Vector2(rB.velocity.x, jumpForce);
        }
    }

    void Update()
    {
        if(CanMove)
        {
            HandleMovementInput();
            PlayerStateMachine();
            PlayerAnimationHandler();
        }
        CheckPlayerLookingDirection();

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
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
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
        }
    }

    /// <summary>
    /// This is used to memorize the player facing direction for the bark
    /// </summary>
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
    }
}
