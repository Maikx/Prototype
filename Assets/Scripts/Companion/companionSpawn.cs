using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class companionSpawn : MonoBehaviour
{
    public GameObject companion;
    public GameObject trigger;

    void OnTriggerEnter2D(Collider2D col)
    {
        companion.SetActive(true);
        companion.transform.position = col.transform.position;
        trigger.SetActive(false);
    }
}
