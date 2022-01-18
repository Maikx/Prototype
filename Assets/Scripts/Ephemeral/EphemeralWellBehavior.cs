using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EphemeralWellBehavior : MonoBehaviour
{
    [HideInInspector] public bool isTouched;

    [Header("Ephemeral")]
    public GameObject ephemeralPrefab;
    public List<GameObject> ephemerals;

    [Header("Function Options")]
    [SerializeField] public bool active = true;

    [Header("Parameters")]
    public float spawnHeight;

    private void Start()
    {
        CreateEphemeral();
    }

    private void Update()
    {
        CompanionInteraction();
    }

    void CompanionInteraction()
    {
        if(Input.GetMouseButtonDown(0) && isTouched)
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
        if (active)
        {
            ephemerals.Add(Instantiate(ephemeralPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + spawnHeight), Quaternion.identity) as GameObject);
        }
    }

    private void Reset()
    {
        if (ephemerals.Count >= 1)
        {
            Destroy(ephemerals[ephemerals.Count - 1].gameObject);
            for (var i = ephemerals.Count - 1; i > -1; i--)
            {
                if (ephemerals[i] != null)
                {
                    ephemerals.RemoveAt(ephemerals.Count - 1);
                    CreateEphemeral();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            isTouched = true;
        }

        if (collision.tag == "Ephemeral")
        {
            if(!active)
            {
                active = true;
                ephemerals.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            isTouched = false;
        }
    }
}
