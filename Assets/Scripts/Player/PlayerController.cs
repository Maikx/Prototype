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
    [HideInInspector] public Animator anim;
    private enum FacingDirectionHorizontal { Left, Right }
    private enum FacingDirectionVertical { Up, Down, None }
    [HideInInspector] public HealthManager healthManager;
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public CheckPointManager checkPointManager;

    public bool CanMove { get; private set; } = true;
    public bool IsMoving { get; private set; }
    private bool isRunning => canRun && Input.GetKey(sprintKey);
    private bool isJumping => canJump && Input.GetKey(jumpKey);
    private bool isBarking => canBark && Input.GetKey(barkKey);

    [Header("Function Options")]
    [SerializeField] private bool canRun = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canBark = true;
    [SerializeField] FacingDirectionHorizontal currentDirectionHorintal = FacingDirectionHorizontal.Right;
    [SerializeField] [HideInInspector] FacingDirectionVertical currentDirectionVertical = FacingDirectionVertical.None;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode barkKey = KeyCode.B;

    [Header("Movement Parameters")]
    public float walkSpeed = 8;
    public float grabSpeed = 4;
    public float airborneSpeed = 4;
    public float jumpDelay = 2;
    public float runSpeed = 20;
    public float timeZeroToMax = 2.5f;
    public float jumpForce = 10;
    public float gravity = -20;
    [HideInInspector] private Vector3 direction;
    [HideInInspector] public float hInput;
    [HideInInspector] public float vInput;
    [HideInInspector] public float accelRatePerSec;
    [HideInInspector] public float currentSpeed;
    [HideInInspector] public float jumpTimer = 0;

    [Header("Fusion Parameters")]
    public float fusedWalkSpeed = 12;
    public float fusedJumpForce = 15;

    [Header("Misc Parameters")]
    public LayerMask groundLayer;
    public GameObject companionPrefab;
    public Transform groundCheck;
    [HideInInspector]public bool isGrounded;
    [HideInInspector]public bool isFused;

    public GameObject transparentObject;
    public Vector3 transparentObjectSize;


    void Awake()
    {
        rB = gameObject.GetComponent<Rigidbody2D>();
        oG = gameObject.GetComponent<ObjectGrab>();
        bI = gameObject.GetComponent<BarkInteraction>();
        anim = gameObject.GetComponent<Animator>();
        Physics.SyncTransforms();
    }

    void Start()
    {
        healthManager = GameObject.FindObjectOfType<HealthManager>();
        checkPointManager = GameObject.FindObjectOfType<CheckPointManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        transform.position = gameManager.PlayerRestartPos;

        //This is used to gain speed overtime
        accelRatePerSec = runSpeed / timeZeroToMax;
    }


    void Update()
    {
        if(CanMove)
        {
            HandleMovementInput();
            PlayerAnimator();
            CheckIfCanDoStuff();
        }
        CheckPlayerLookingDirection();

        //test HealthStystem
        if (Input.GetKeyDown(KeyCode.K)) healthManager.OnDamage(1);
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!healthManager.playerIsLive)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        TransparentObjcetResizing();
    }

    void TransparentObjcetResizing()
    {
        transparentObject.gameObject.transform.localScale = transparentObjectSize;
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
            if (!isFused) currentSpeed = walkSpeed;
            else currentSpeed = fusedWalkSpeed;
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
                if (!isFused) rB.velocity = new Vector2(rB.velocity.x, jumpForce);
                else rB.velocity = new Vector2(rB.velocity.x, fusedJumpForce);
            }
        }

        rB.velocity = new Vector2(direction.x, rB.velocity.y);

        if (Input.GetKey(barkKey) && isGrounded)
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

        if (Input.GetMouseButtonDown(0) && isFused)
        {
            Instantiate(companionPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 2), Quaternion.identity);
            isFused = false;
        }
    }

    void CheckIfCanDoStuff()
    {
        if (isGrounded && !oG.isGrabbed && !isJumping) canRun = true;
        else canRun = false;

        if (jumpTimer < 0 && !oG.isGrabbed) canJump = true;
        else canJump = false;

        if (isGrounded) canBark = true;
        else canBark = false;
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

    public void CompanionFusion()
    {
        isFused = true;
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
