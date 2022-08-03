using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EphemeralWellBehavior : MonoBehaviour
{
    [HideInInspector] public bool isTouched;
    [HideInInspector] public GameObject ephemeral;

    [Header("Ephemeral")]
    public GameObject ephemeralPrefab;

    [Header("Function Options")]
    [SerializeField] public bool active = true;

    [Header("Parameters")]
    public float spawnHeight;

    public AudioSource ActiveSound;

    private void Start()
    {
        CreateEphemeral();
    }

    /// <summary>
    /// This will create an ephemeral if the well is active
    /// </summary>
    public void CreateEphemeral()
    {
        if (active)
        {
            ephemeral = Instantiate(ephemeralPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + spawnHeight), Quaternion.identity);
        }
    }

    /// <summary>
    /// This will happen if the ephemeral will reach the next well
    /// </summary>
    public void DeactivateWell()
    {
        ephemeral = null;
        active = false;
    }

    /// <summary>
    /// This will destroy the current ephemeral and create a new one
    /// </summary>
    public void Reset()
    {
        Destroy(ephemeral);
        CreateEphemeral();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ephemeral")
        {
            if(!active)
            {
                active = true;
                collision.GetComponent<EphemeralBehavior>().WellChange();
                ephemeral = collision.gameObject;
                ActiveSound.Play();
            }
        }
    }
}
