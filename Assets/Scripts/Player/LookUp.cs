using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookUp : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    public void Start()
    {
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    public void Update()
    {
        CameraZoom();
    }    

    void CameraZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            animator.SetBool("isZoomActive", true);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            //animator.SetBool("isZoomActive", false);
        }
    }
}
