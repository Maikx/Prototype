using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    private Camera cam;
    public GameObject companion;
    public float depth = 5.0f;

    private void Awake()
    {
        companion = GameObject.Find("Companion");
    }

    void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth));
        companion.transform.position = point;
    }
}
