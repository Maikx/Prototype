using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class companionSpawn : MonoBehaviour
{
    public GameObject companion;
    public GameObject triggerNumber2;

    void Start ()
    {
        triggerNumber2.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        triggerNumber2.SetActive(false);
        companion.SetActive(true);
    }
}
