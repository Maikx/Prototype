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
    private float currentBoundary;
    public float minBoundary = 5.0f;
    public float maxBoundary = 10.0f;
    public float movementSpeed = 10f;
    public float scaleSpeed = 2;
    private bool resize;
    [HideInInspector]public Vector3 point;

    private void Update()
    {
        Boundary();
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

    void OnTriggerStay2D(Collider2D other)
    {
        //This will be used for interactable objects..
        if(other.tag == "CompanionInteractableObject" && Input.GetKey(interactKey))
        {
            Debug.Log("Interacted!");
        }
    }
}
