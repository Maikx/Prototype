using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookUp : MonoBehaviour
{
    public Animator animator;
    private bool isLookingUp = false;
    private bool isLookingDown = false;

    public void Update()
    {
        PlayerLookUp();
        PlayerLookDown();
        CameraZoom();
    }

    void PlayerLookUp()
    {
        if (Input.GetKeyDown(KeyCode.W) && isLookingUp == false)
        {
            animator.SetBool("LookUp", true);
            isLookingUp = true;
        }
        else if (Input.GetKeyUp(KeyCode.W) && isLookingUp == true)
        {
            animator.SetBool("LookUp", false);
            isLookingUp = false;
        }
    }

    void PlayerLookDown()
    {
        if (Input.GetKeyDown(KeyCode.S) && isLookingDown == false)
        {
            animator.SetBool("LookDown", true);
            isLookingDown = true;
        }
        else if (Input.GetKeyUp(KeyCode.S) && isLookingDown == true)
        {
            animator.SetBool("LookDown", false);
            isLookingDown = false;
        }
    }

    void CameraZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            animator.SetBool("isZoomActive", true);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            animator.SetBool("isZoomActive", false);
        }
    }
}
