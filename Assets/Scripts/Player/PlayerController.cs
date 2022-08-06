using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ObjectGrab))]
[RequireComponent(typeof(BarkInteraction))]

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public ObjectGrab oG;
    [HideInInspector] public BarkInteraction bI;
    [HideInInspector] public Rigidbody2D rB;
    [HideInInspector] public HealthManager healthManager;
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public Animator stateMachine;
    private SoundTracker sT;

    public Animator animHandler;
    public enum FacingDirectionHorizontal { Left, Right }
    private enum FacingDirectionVertical { Up, Down, None }

    public bool CanMove;
    public bool IsMoving { get; private set; }
    private bool isJumping => canJump && Input.GetKey(jumpKey);
    public bool isBarking => canBark && Input.GetKey(barkKey);

    [Header("Function Options")]
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canBark = true;
    [HideInInspector]public bool canMoveScript = true;
    [SerializeField] [HideInInspector] FacingDirectionHorizontal lastKnownFacingDirection;
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
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float currentSpeed;
    [HideInInspector] public float hInput;
    [HideInInspector] public float vInput;

    [Header("Timers")]
    public float timeAfterJumpAgain;
    public float timeToTurnAround;
    public float timeToInteract;
    public float timeToBark;
    public float timeWhileDead;
    private float currentJumpTime;
    [HideInInspector] public float currentDelayTime;

    [Header("Physics Parameters")]
    public float acceleration;
    public float friction;
    public float gravity;
    [HideInInspector] public float currentGravity;

    [Header("Misc Parameters")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Transform groundCheckBack;
    public GameObject transparentObject;
    public Vector3 transparentObjectSize;
    public bool playerIsGrabbed;
    public float FallingThreshold = -0.001f;
    private bool animIsJumping;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isDead;
    [HideInInspector] public bool hasGroundBehind;
    [HideInInspector] public Thorns thorns;
    private float oldPosY;
    [HideInInspector] public bool isFalling = false;
    [HideInInspector] public PhysicsMaterial2D pM2D;


    void Awake()
    {
        rB = gameObject.GetComponent<Rigidbody2D>();
        pM2D = rB.sharedMaterial;
        oG = gameObject.GetComponent<ObjectGrab>();
        bI = gameObject.GetComponent<BarkInteraction>();
        stateMachine = gameObject.GetComponent<Animator>();
        sT = gameObject.GetComponent<SoundTracker>();
        Physics.SyncTransforms();
    }

    void Start()
    {
        healthManager = GameObject.FindObjectOfType<HealthManager>();
        thorns = GameObject.FindObjectOfType<Thorns>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        transform.position = GameManager.instance.RestartPlayerPosition; // <- delete this once tested
        lastKnownFacingDirection = currentDirectionHorintal;
        canMoveScript = true;
        currentGravity = gravity;
        oldPosY = transform.position.y;
    }

    private void FixedUpdate()
    {
        HandleMovementInput();
        //This is the sphere that checks if the player is grounded.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, grounCheckSize, groundLayer);

        hasGroundBehind = Physics2D.OverlapCircle(groundCheckBack.position, 0.15f, groundLayer);

        if (rB != null)
        {
            //This HandlesPlayerMovement
            rB.velocity += new Vector2(direction.x * acceleration * Time.deltaTime, 0);
            rB.velocity -= new Vector2(rB.velocity.x * friction * Time.deltaTime, currentGravity * Time.deltaTime);

            //This plays when the player jumps
            if (Input.GetKey(jumpKey) && isGrounded && !oG.isGrabbed && canJump && currentDelayTime <= 0)
            {
                rB.velocity = new Vector2(rB.velocity.x, jumpForce);
                currentJumpTime = timeAfterJumpAgain;
                animIsJumping = true;
            }
        }
        else if (rB == null && !playerIsGrabbed)
        {
            rB = gameObject.GetComponent<Rigidbody2D>();
            rB.freezeRotation = true;
            rB.sharedMaterial = pM2D;
            rB.gravityScale = 0;
            rB.angularDrag = 0;
        }
    }

    void Update()
    {
        PlayerStateMachine();
        PlayerAnimationHandler();
        CheckPlayerLookingDirection();
        CheckIfMovingBackGrabbed();
        JumpTimer();
        TurnAround();
        CheckIfFalling();
        Death();

        if (Input.GetKeyDown(KeyCode.L)) // <- only for testing!
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (thorns != null && thorns.OnTouchTrap() == true)
        {
            GameManager.instance.SetHealth(0);
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
        if (CanMove && canMoveScript)
        {
            hInput = Input.GetAxis("Horizontal");
            vInput = Input.GetAxis("Vertical");
        }
        else
        {
            hInput = 0;
            vInput = 0;
            canJump = false;
        }

        direction.x = hInput * (currentSpeed);
    }

    /// <summary>
    /// This is used to memorize the player facing direction for the bark
    /// </summary>
    public void CheckPlayerLookingDirection()
    {
        if (hInput > 0 && !oG.isGrabbed && canMoveScript)
            currentDirectionHorintal = FacingDirectionHorizontal.Right;

        else if (hInput < 0 && !oG.isGrabbed && canMoveScript)
            currentDirectionHorintal = FacingDirectionHorizontal.Left;

        else if (vInput > 0 && !oG.isGrabbed && canMoveScript)
            currentDirectionVertical = FacingDirectionVertical.Up;
        else if (vInput < 0 && !oG.isGrabbed && canMoveScript)
            currentDirectionVertical = FacingDirectionVertical.Down;
        else if (vInput == 0 && !oG.isGrabbed && canMoveScript)
            currentDirectionVertical = FacingDirectionVertical.None;
    }

    public void Bark()
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
                {
                    bI.BarkRight();
                }
                else if (currentDirectionHorintal == FacingDirectionHorizontal.Left)
                {
                    bI.BarkLeft();
                }
                break;
        }
        sT.BarkSound();
    }

    /// <summary>
    /// This is used to memorize if the player moving backwards in grabbed state
    /// </summary>
    public void CheckIfMovingBackGrabbed()
    {
        if (oG.isGrabbed && currentDirectionHorintal == FacingDirectionHorizontal.Right && hInput < 0 || oG.isGrabbed && currentDirectionHorintal == FacingDirectionHorizontal.Left && hInput > 0)
            oG.isMovingBackGrabbed = true;
        else if(oG.isGrabbed && currentDirectionHorintal == FacingDirectionHorizontal.Right && hInput >= 0 || oG.isGrabbed && currentDirectionHorintal == FacingDirectionHorizontal.Left && hInput <= 0)
            oG.isMovingBackGrabbed = false;
    }

    void JumpTimer()
    {
        if (canMoveScript && CanMove)
        {
            if (currentJumpTime > 0)
            {
                canJump = false;
                animIsJumping = false;
                currentJumpTime -= Time.deltaTime;
            }
            else if (currentJumpTime <= 0)
            {
                canJump = true;
            }
        }
    }

    void TurnAround()
    {
        if (lastKnownFacingDirection != currentDirectionHorintal)
        {
            if (isGrounded)
            {
                oG.canInteract = false;
                stateMachine.SetTrigger("isTurning");
                animHandler.SetTrigger("isTurning");
            }
            else
            {
                RotateModel();
            }
            lastKnownFacingDirection = currentDirectionHorintal;
        }
    }

    public void RotateModel()
    {
        if (currentDirectionHorintal == FacingDirectionHorizontal.Right)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            oG.canInteract = true;
        }
        else if (currentDirectionHorintal == FacingDirectionHorizontal.Left)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
            oG.canInteract = true;
        }
    }

    void CheckIfFalling()
    {
        /*if (oldPosY != gameObject.transform.position.y && gameObject.transform.position.y < oldPosY)
        {
            isFalling = true;
            oldPosY = gameObject.transform.position.y;
        }
        else if ()
        {
            isFalling = false;
        }*/

        float distancePerSecondSinceLastFrame = (transform.position.y - oldPosY) * Time.deltaTime;
        oldPosY = gameObject.transform.position.y;

        if (distancePerSecondSinceLastFrame < FallingThreshold)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
    }

    void Death()
    {
        if(GameManager.instance.health == 0 && isDead == false)
        {
            isDead = true;
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
        stateMachine.SetBool("isInteracting", oG.isInteracting);
        stateMachine.SetInteger("objectType", oG.objectType);
        stateMachine.SetBool("isBarking", isBarking);
        stateMachine.SetBool("playerIsGrabbed", playerIsGrabbed);
        stateMachine.SetBool("isDead", isDead);
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
        animHandler.SetBool("isFalling", isFalling);
        animHandler.SetBool("playerIsGrabbed", playerIsGrabbed);
        animHandler.SetBool("isDead", isDead);
        if (animIsJumping) animHandler.SetTrigger("isJumping");
    }
}
