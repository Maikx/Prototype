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

    void OnTriggerEnter()
    {
        companion.SetActive(true);
    }
}
