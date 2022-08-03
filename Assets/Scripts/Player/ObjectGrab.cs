using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [HideInInspector] public bool isInteracting => canInteract && Input.GetKey(interactKey);
    [HideInInspector] public bool canInteract = true;
    [HideInInspector] public bool isGrabbed;
    [HideInInspector] public bool isMovingBackGrabbed;
    [HideInInspector] public BoxCollider2D playerCollider;
    [HideInInspector] public Vector3 point;
    public int objectType;
    public Transform grabDetect;
    public Transform objectHolder;
    public float rayDist;

    public AudioSource GrabSound;

    private void Start()
    {
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        PlayerGrabObject();
    }

    /// <summary>
    /// This checks if in front of the player there is an interactable object & allows it to be grabbed.
    /// </summary>
    public void PlayerGrabObject()
    {
        if (grabDetect != null)
        {
            //The Raycast is used to check if there is a rigidbody that the player can grab.
            RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);

            if (grabCheck.collider != null && grabCheck.collider.gameObject.layer == 11)
            {
                if (grabCheck.collider.gameObject.GetComponent<ObjectBehavior>().objectType == ObjectBehavior.ObjectType.Box)
                {
                    objectType = 1;
                    //Player grabs object.
                    if (Input.GetKey(interactKey) && grabCheck.collider.GetComponent<ObjectBehavior>().isGrounded && gameObject.GetComponent<PlayerController>().isGrounded && gameObject.GetComponent<PlayerController>().hasGroundBehind && canInteract)
                    {
                        isGrabbed = true;
                        grabCheck.collider.gameObject.transform.parent = objectHolder;
                        if (grabCheck.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                        {
                            Destroy(grabCheck.collider.gameObject.GetComponent<Rigidbody2D>());
                            GrabSound.Play();
                        }
                    }
                    //Player releases object.
                    else
                    {
                        isGrabbed = false;
                        if (grabCheck.collider.gameObject.GetComponent<Rigidbody2D>() == null)
                        {
                            grabCheck.collider.gameObject.AddComponent<Rigidbody2D>();
                            grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().mass = 10000;
                            GrabSound.Stop();
                        }
                        grabCheck.collider.gameObject.transform.parent = null;
                    }
                }
                else if (grabCheck.collider.gameObject.GetComponent<ObjectBehavior>().objectType == ObjectBehavior.ObjectType.Rope && canInteract)
                {
                    objectType = 2;
                    if (Input.GetKey(interactKey))
                    {
                        grabCheck.collider.gameObject.GetComponent<ObjectBehavior>().Activate();
                    }
                }
            }
            else
            {
                objectType = 0;
            }
        }
    }
}
