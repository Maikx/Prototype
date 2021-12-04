using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    [HideInInspector] public CapsuleCollider2D playerCollider;
    [HideInInspector] public Transform grabDetect;
    [HideInInspector] public Transform objectHolder;
    [HideInInspector] public bool isGrabbed;
    public float rayDist;

    private void Start()
    {
        playerCollider = gameObject.GetComponent<CapsuleCollider2D>();
        grabDetect = gameObject.transform.Find("GrabDetect");
        objectHolder = gameObject.transform.Find("ObjectHolder");
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

            if (grabCheck.collider != null && grabCheck.collider.tag == "InteractableObject")
            {
                //Player grabs object.
                if (Input.GetKey(KeyCode.E) && grabCheck.collider.GetComponent<ObjectBehavior>().isGrounded)
                {
                    isGrabbed = true;
                    Physics2D.IgnoreCollision(grabCheck.collider, playerCollider);
                    grabCheck.collider.gameObject.transform.parent = objectHolder;
                    grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().mass = 10;
                }
                //Player releases object.
                else
                {
                    isGrabbed = false;
                    grabCheck.collider.gameObject.transform.parent = null;
                    grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().mass = 10000;
                }
            }
        }
    }
}
