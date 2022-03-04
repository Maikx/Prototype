using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appareUnCompagno : MonoBehaviour
{
    public GameObject companion;

    public void Awake()
    {
        companion.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        companion.SetActive(true);
    }
}
