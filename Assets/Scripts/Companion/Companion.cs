using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    public Camera cam;
    public GameObject player;

    [Header("Function Options")]
    private float t0;
    private bool shortClick;

    [Header("Parameters")]
    private float currentBoundary;
    public float minBoundary;
    public float maxBoundary;
    public float movementSpeed;
    public float scaleSpeed;

    [HideInInspector]public Vector3 point;
    [HideInInspector]public GameObject interactable;
    [HideInInspector] public bool isGrabbed;

    private void Start()
    {
        t0 = 0;
        shortClick = false;
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        CompanionInteraction();
        MouseShortClick();
        Boundary();
        CompanionAnimator();
    }

    void MouseShortClick()
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

        Vector3 direction = (Vector3)(Input.mousePosition - point);
        direction.Normalize();

        this.GetComponent<Rigidbody2D>().velocity = direction * movementSpeed;
    }

    /// <summary>
    /// Makes companion move with cursor & specifies distance from bond object (player)
    /// </summary>
    public void Boundary()
    {
        if (gameObject.transform.position.x > player.transform.position.x + currentBoundary)
            gameObject.transform.position = new Vector2(player.transform.position.x + currentBoundary, gameObject.transform.position.y);

        else if (gameObject.transform.position.x < player.transform.position.x - currentBoundary)
            gameObject.transform.position = new Vector2(player.transform.position.x - currentBoundary, gameObject.transform.position.y);

        if (player.GetComponent<PlayerController>().hInput != 0)
            currentBoundary = Mathf.Lerp(currentBoundary, minBoundary, Time.deltaTime * scaleSpeed);
        else
            currentBoundary = Mathf.Lerp(currentBoundary, maxBoundary, Time.deltaTime * scaleSpeed);
    }

    private void CompanionInteraction()
    {
        if (Input.GetMouseButtonDown(0) && interactable != null)
        {
            if (interactable.gameObject.tag != "Player")
                interactable.GetComponent<CompanionInteractableBehavior>().HeldInteract();
            
            if (interactable.gameObject.tag == "Player")
            {
                Debug.Log("Grabbed");
                player.GetComponent<PlayerController>().CanMove = false;
                player.GetComponent<Rigidbody2D>().isKinematic = true;
                isGrabbed = true;
                player.transform.parent = gameObject.transform;
            }
        }

        else if (Input.GetMouseButtonUp(0) && interactable != null)
        {
            if (interactable.gameObject.tag != "Player")
                interactable.GetComponent<CompanionInteractableBehavior>().HeldInteractStop();
            
            if (interactable.gameObject.tag == "Player")
            {
                Debug.Log("Not Grabbed");
                player.GetComponentInParent<PlayerController>().CanMove = true;
                player.GetComponent<Rigidbody2D>().isKinematic = false;
                isGrabbed = false;
                player.transform.parent = null;
            }
        }

        else if (shortClick && interactable != null)
        {
            interactable.GetComponent<CompanionInteractableBehavior>().Interact();
            shortClick = false;
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
        if (collision.gameObject.layer == 12 || collision.gameObject.tag == "Player")
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
