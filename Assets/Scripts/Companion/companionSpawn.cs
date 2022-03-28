using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class companionSpawn : MonoBehaviour
{
    public GameObject companion;

    void Start ()
    {
        companion.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        companion.SetActive(true);
    }
}
