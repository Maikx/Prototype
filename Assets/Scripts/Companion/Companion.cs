using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    [Header("Set These GameObjects!")]
    public GameObject player;
    private Camera cam;
    private Renderer _renderer;

    [Header("Function Options")]
    private float t0;
    private bool shortClick;

    [Header("Parameters")]
    public float movementSpeed;
    private float currentBoundary;
    public float minBoundary;
    public float maxBoundary;
    public float scaleSpeed;
    public float decreaseSpeedDistance;
    public float stopDistance;


    [Header("Grab Parameters")]
    public float grabMovementSpeed;
    public float timeAfterBlinking;
    public float timeAfterDropping;
    private float currentTime;

    [HideInInspector]public Vector3 point;
    [HideInInspector]public GameObject interactable;
    [HideInInspector]public bool canGrab;
    [HideInInspector]public bool playerIsGrabbed;
    [HideInInspector]public bool isBlinking;
    [HideInInspector] public Vector3 direction;


    private void Start()
    {
        t0 = 0;
        shortClick = false;
        cam = Camera.main;
        _renderer = gameObject.GetComponent<Renderer>();
        currentTime = timeAfterDropping;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        LeftMouseInteraction();
        RightMouseInteraction();
        LeftMouseShortClick();
        //Boundary();
        CompanionAnimator();
        Timer();
        BlinkEffect();
    }

    void LeftMouseShortClick()
    {
        if (Input.GetMouseButtonDown(0))
            t0 = Time.time;

        else if (Input.GetMouseButtonUp(0) && (Time.time - t0) < 0.3f)
        {
            shortClick = true;
        }
    }

    public void Movement()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        point = Camera.main.WorldToScreenPoint(transform.position);

        direction = (Input.mousePosition - point);
        direction.Normalize();

        if (Vector3.Distance(point, Input.mousePosition) > decreaseSpeedDistance)
        {
            if (playerIsGrabbed == false) this.GetComponent<Rigidbody2D>().velocity = direction * movementSpeed;
            else this.GetComponent<Rigidbody2D>().velocity = direction * grabMovementSpeed;
        }
        else if (Vector3.Distance(point, Input.mousePosition) < decreaseSpeedDistance)
        {
            if (Vector3.Distance(point, Input.mousePosition) <= stopDistance)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            else
            {
                float deceleration = Vector3.Distance(point, Input.mousePosition) / 10;
                this.GetComponent<Rigidbody2D>().velocity = direction * deceleration;
            }
        }
    }

    /// <summary>
    /// Makes companion move with cursor & specifies distance from bond object (player)
    /// </summary>
    //public void Boundary()
    //{
        //if (gameObject.transform.position.x > player.transform.position.x + currentBoundary)
            //gameObject.transform.position = new Vector2(player.transform.position.x + currentBoundary, gameObject.transform.position.y);

        //else if (gameObject.transform.position.x < player.transform.position.x - currentBoundary)
            //gameObject.transform.position = new Vector2(player.transform.position.x - currentBoundary, gameObject.transform.position.y);

        //if (player.GetComponent<PlayerController>().hInput != 0)
            //currentBoundary = Mathf.Lerp(currentBoundary, minBoundary, Time.deltaTime * scaleSpeed);
        //else
            //currentBoundary = Mathf.Lerp(currentBoundary, maxBoundary, Time.deltaTime * scaleSpeed);
    //}


    private void RightMouseInteraction()
    {
        if (Input.GetMouseButtonDown(1) && interactable != null && interactable.gameObject.tag == "Player" && canGrab == true)
        {
                playerIsGrabbed = true;
                currentTime = timeAfterDropping;
                player.GetComponent<PlayerController>().CanMove = false;
                player.GetComponent<PlayerController>().playerIsGrabbed = true;
                player.GetComponent<Rigidbody2D>().isKinematic = true;
                player.transform.parent = gameObject.transform;
        }

        else if (Input.GetMouseButtonUp(1) && interactable != null && interactable.gameObject.tag == "Player" || interactable != null && currentTime == 0)
        {
                playerIsGrabbed = false;
                player.GetComponentInParent<PlayerController>().CanMove = true;
                player.GetComponent<PlayerController>().playerIsGrabbed = false;
                player.GetComponent<Rigidbody2D>().isKinematic = false;
                player.transform.parent = null;
        }
    }

    private void LeftMouseInteraction()
    {
        if (Input.GetMouseButtonDown(0) && interactable != null && interactable.gameObject.tag != "Player" && canGrab)
        {
                interactable.GetComponent<CompanionInteractableBehavior>().HeldInteract();
        }

        else if (Input.GetMouseButtonUp(0) && interactable != null && interactable.gameObject.tag != "Player")
        {
                interactable.GetComponent<CompanionInteractableBehavior>().HeldInteractStop();
        }

        else if (shortClick && interactable != null && interactable.gameObject.tag != "Player")
        {
            interactable.GetComponent<CompanionInteractableBehavior>().Interact();
            shortClick = false;
        }
    }

    void BlinkEffect()
    {
        if (isBlinking == true)
        {
            if (Time.fixedTime % .5 < .2)
            {
                _renderer.enabled = false;
            }
            else
            {
                _renderer.enabled = true;
            }
        }
        else
        {
            _renderer.enabled = true;
        }
    }

    void Timer()
    {
        if (playerIsGrabbed)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < timeAfterBlinking) isBlinking = true;
        }
        else
        {
            currentTime += Time.deltaTime;
            if (currentTime > timeAfterBlinking) isBlinking = false;
        }

        if (currentTime < 0) currentTime = 0;
        else if (currentTime > timeAfterDropping) currentTime = timeAfterDropping;

        if (currentTime >= timeAfterDropping)
        {
            canGrab = true;
        }
        else
        {
            canGrab = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            interactable = collision.gameObject;
        }

        else if (collision.gameObject.tag == "Player")
        {
            interactable = collision.gameObject;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            interactable = null;
        }

        else if (collision.gameObject.tag == "Player")
        {
            interactable = null;
        }
    }

    /// <summary>
    /// This manages the companion's movement animator, not the animations!
    /// </summary>
    public void CompanionAnimator()
    {
        //if (hInput != 0 || vInput != 0) anim.SetBool("isMoving", true);
        //else anim.SetBool("isMoving", false);
        //anim.SetBool("isGrounded", isGrounded);
        //anim.SetBool("isGrabbed", oG.isGrabbed);
    }
}
