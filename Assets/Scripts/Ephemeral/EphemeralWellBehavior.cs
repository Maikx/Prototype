using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EphemeralWellBehavior : MonoBehaviour
{
    public GameObject ephemeralPrefab;
    public List <GameObject> ephemerals;
    public float spawnHeight;


    private void Update()
    {
        for (var i = ephemerals.Count - 1; i > -1; i--)
        {
            if (ephemerals[i] == null)
                ephemerals.RemoveAt(i);
        }
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && ephemerals.Count == 0)
        {
            ephemerals.Add(Instantiate(ephemeralPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + spawnHeight), Quaternion.identity) as GameObject);
        }
    }
}
