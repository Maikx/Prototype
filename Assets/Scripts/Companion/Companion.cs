using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    private bool isInteracting => canInteract && Input.GetKey(interactKey);

    public Camera cam;
    public GameObject player;

    [Header("Function Options")]
    [SerializeField] private bool canInteract = true;

    [Header("Controls")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [Header("Parameters")]
    public float depth = 5.0f;
    private float currentBoundary;
    public float minBoundary = 5.0f;
    public float maxBoundary = 10.0f;
    public float scaleSpeed = 2;
    private bool resize;
    private Vector3 point;
    public float rayDist;

    void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Boundary();
        CheckWalls();
    }

    /// <summary>
    /// Makes companion move with cursor & specifies distance from bond object (player)
    /// </summary>
    public void Boundary()
    {
        Vector3 mousePos = Input.mousePosition;
        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth + 10.0f));

        if (point.x > player.transform.position.x + currentBoundary)
            point.x = player.transform.position.x + currentBoundary;
        else if (point.x < player.transform.position.x - currentBoundary)
            point.x = player.transform.position.x - currentBoundary;

        gameObject.transform.position = point;

        if (player.GetComponent<PlayerController>().hInput != 0)
            resize = true;
        else
            resize = false;

        if (resize)
            currentBoundary = Mathf.Lerp(currentBoundary, minBoundary, Time.deltaTime * scaleSpeed);
        else
            currentBoundary = Mathf.Lerp(currentBoundary, maxBoundary, Time.deltaTime * scaleSpeed);
    }

    /// <summary>
    /// Checks if walls are nearby..
    /// </summary>
    public void CheckWalls()
    {
            //The Raycast is used to check if there is a rigidbody that the player can grab.
            RaycastHit2D right = Physics2D.Raycast(gameObject.transform.position, Vector2.right * transform.localScale, rayDist);
            RaycastHit2D left = Physics2D.Raycast(gameObject.transform.position, Vector2.left * transform.localScale, rayDist);
            RaycastHit2D up = Physics2D.Raycast(gameObject.transform.position, Vector2.up * transform.localScale, rayDist);
            RaycastHit2D down = Physics2D.Raycast(gameObject.transform.position, Vector2.down * transform.localScale, rayDist);

        if (right.collider != null && right.collider.gameObject.layer == 8)
        {
            //Debug.Log("Wall Right!");
        }
        if (left.collider != null && left.collider.gameObject.layer == 8)
        {
            //Debug.Log("Wall Left!");
        }
        if (up.collider != null && up.collider.gameObject.layer == 8)
        {
            //Debug.Log("Wall Up!");
        }
        if (down.collider != null && down.collider.gameObject.layer == 8)
        {
            //Debug.Log("Wall Down!");
        }

    }


    void OnTriggerStay2D(Collider2D other)
    {
        //This will be used for interactable objects..
        if(other.tag == "CompanionInteractableObject" && Input.GetKey(interactKey))
        {

        }
    }
}
