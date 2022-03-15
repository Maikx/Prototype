using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    private bool isInteracting => canInteract && Input.GetKey(interactKey);
    [SerializeField] private bool canInteract = true;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [HideInInspector] public BoxCollider2D playerCollider;
    public Transform grabDetect;
    public Transform objectHolder;
    [HideInInspector] public bool isGrabbed;
    public float rayDist;
    [HideInInspector] public Vector3 point;

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

            if(grabCheck == true)
                Debug.Log(grabCheck);

            if (grabCheck.collider != null && grabCheck.collider.gameObject.layer == 11)
            {
                if (grabCheck.collider.gameObject.GetComponent<ObjectBehavior>().objectType == ObjectBehavior.ObjectType.Box)
                {
                    //Player grabs object.
                    if (Input.GetKey(interactKey) && grabCheck.collider.GetComponent<ObjectBehavior>().isGrounded && gameObject.GetComponent<PlayerController>().isGrounded)
                    {
                        isGrabbed = true;
                        grabCheck.collider.gameObject.transform.parent = objectHolder;
                        if (grabCheck.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                        {
                            Destroy(grabCheck.collider.gameObject.GetComponent<Rigidbody2D>());
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
                        }
                        grabCheck.collider.gameObject.transform.parent = null;
                    }
                }
                else if (grabCheck.collider.gameObject.GetComponent<ObjectBehavior>().objectType == ObjectBehavior.ObjectType.Rope)
                {
                    if (Input.GetKey(interactKey))
                    {
                        grabCheck.collider.gameObject.GetComponent<ObjectBehavior>().Activate();
                    }
                }
            }
        }
    }
}
