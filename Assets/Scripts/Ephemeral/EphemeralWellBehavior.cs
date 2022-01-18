using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EphemeralWellBehavior : MonoBehaviour
{
    [Header("Ephemeral")]
    public GameObject ephemeralPrefab;
    public List<GameObject> ephemerals;

    [Header("Function Options")]
    [SerializeField] public bool active = true;
    [SerializeField] public bool ephemeral;

    [Header("Parameters")]
    public float spawnHeight;

    private void Start()
    {
        CreateEphemeral();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Reset();
        }
    }

    /// <summary>
    /// Checks if listed ephemeral is null..
    /// </summary>
    //private void CheckIfNull()
    //{
    //    for (var i = ephemerals.Count - 1; i > -1; i--)
    //    {
    //        if (ephemerals[i] == null)
    //            ephemerals.RemoveAt(i);
    //    }
    //}

    public void CreateEphemeral()
    {
        if (ephemerals.Count == 0 && active)
        {
            ephemerals.Add(Instantiate(ephemeralPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + spawnHeight), Quaternion.identity) as GameObject);
        }
    }

    private void Reset()
    {
        Destroy(ephemerals[ephemerals.Count - 1].gameObject);
        ephemerals.RemoveAt(ephemerals.Count - 1);

        CreateEphemeral();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && Input.GetMouseButtonDown(0))
        {
            Reset();
        }

        if (collision.tag == "Ephemeral")
        {
            if (ephemerals.Contains(GameObject.Find("Ephemeral(Clone)"))) 
            { 
            
            }

            else
            {
                active = true;
                ephemerals.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && Input.GetMouseButtonDown(0))
        {
            Reset();
        }
    }
}
