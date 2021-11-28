using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    public Transform grabDetect;
    public Transform boxHolder;
    public bool isGrabbed;
    public float rayDist;


    private void Update()
    {
        if (grabDetect != null)
        {
            RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);

            if (grabCheck.collider != null && grabCheck.collider.tag == "InteractableObject")
            {
                if (Input.GetKey(KeyCode.E))
                {
                    isGrabbed = true;
                    grabCheck.collider.gameObject.transform.parent = boxHolder;
                    grabCheck.collider.gameObject.transform.position = boxHolder.position;
                    grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().mass = 10;
                }
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
