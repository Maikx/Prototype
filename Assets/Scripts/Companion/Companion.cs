using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    public Camera cam;
    public GameObject player;

    [Header("Function Options")]
    [SerializeField] private bool canInteract = true;
    private float t0;
    private bool shortClick;

    [Header("Parameters")]
    private float currentBoundary;
    public float minBoundary = 5.0f;
    public float maxBoundary = 10.0f;
    public float movementSpeed = 10f;
    public float scaleSpeed = 2;
    private bool resize;

    [HideInInspector]public Vector3 point;
    [HideInInspector]public GameObject interactable;

    private void Start()
    {
        t0 = 0;
        shortClick = false;
    }

    private void Update()
    {
        Boundary();
        CompanionInteraction();
        MouseShortClick();
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

    /// <summary>
    /// Makes companion move with cursor & specifies distance from bond object (player)
    /// </summary>
    public void Boundary()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        point = Camera.main.WorldToScreenPoint(transform.position);

        if (gameObject.transform.position.x > player.transform.position.x + currentBoundary)
            gameObject.transform.position = new Vector2(player.transform.position.x + currentBoundary, gameObject.transform.position.y);

        else if (gameObject.transform.position.x < player.transform.position.x - currentBoundary)
            gameObject.transform.position = new Vector2(player.transform.position.x - currentBoundary, gameObject.transform.position.y);

        Vector3 direction = (Vector3)(Input.mousePosition - point);
        direction.Normalize();

        this.GetComponent<Rigidbody2D>().AddForce(direction * movementSpeed, ForceMode2D.Impulse);

        if (player.GetComponent<PlayerController>().hInput != 0)
            resize = true;
        else
            resize = false;

        if (resize)
            currentBoundary = Mathf.Lerp(currentBoundary, minBoundary, Time.deltaTime * scaleSpeed);
        else
            currentBoundary = Mathf.Lerp(currentBoundary, maxBoundary, Time.deltaTime * scaleSpeed);
    }

    private void CompanionInteraction()
    {
        if (Input.GetMouseButtonDown(0) && interactable != null)
        {
            interactable.GetComponent<CompanionInteractableBehavior>().HeldInteract();
        }

        else if (Input.GetMouseButtonUp(0) && interactable != null)
        {
            interactable.GetComponent<CompanionInteractableBehavior>().HeldInteractStop();
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

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            interactable = null;
        }
    }
}
