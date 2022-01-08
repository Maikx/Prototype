using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    public float depth = 5.0f;
    private float currentBoundary;
    public float minBoundary = 5.0f;
    public float maxBoundary = 10.0f;
    public float scaleSpeed = 2;
    private bool resize;
    private Vector3 point;

    void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Boundary();
    }

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
}
