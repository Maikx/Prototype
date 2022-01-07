using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    private Camera cam;
    public PlayerController pC;
    public GameObject[] boundaries;
    public GameObject companion;
    public float depth = 5.0f;
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
        if (point.x > boundaries[0].transform.position.x && pC.hInput != 0)
            point.x = boundaries[0].transform.position.x;
        else if (point.x < boundaries[1].transform.position.x && pC.hInput != 0)
            point.x = boundaries[1].transform.position.x;

        companion.transform.position = point;
    }
}
