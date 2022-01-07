using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    private Camera cam;
    public GameObject player;
    public GameObject companion;
    public float depth = 5.0f;
    public float boundary = 5.0f;
    public float maxBoundary = 10.0f;
    public Vector3 point;

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
        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth));
        if (point.x > player.transform.position.x + boundary && player.GetComponent<PlayerController>().hInput != 0)
            point.x = player.transform.position.x + boundary;
        else if (point.x < player.transform.position.x - boundary && player.GetComponent<PlayerController>().hInput != 0)
            point.x = player.transform.position.x - boundary;
        else if (point.x > player.transform.position.x + maxBoundary)
            point.x = player.transform.position.x + maxBoundary;
        else if (point.x < player.transform.position.x - maxBoundary)
            point.x = player.transform.position.x - maxBoundary;

        companion.transform.position = point;
    }
}
