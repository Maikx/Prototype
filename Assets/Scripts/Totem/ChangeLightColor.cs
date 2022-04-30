using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightColor : MonoBehaviour
{
    public Color color;
    public GameObject spotlight;
    public Animator totemAnimator;

    [Header("Material")]
    public Material newMaterial;
    public GameObject carattere1, carattere2;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Ephemeral")
        {
            totemAnimator.SetTrigger("TotemUp");
            spotlight.GetComponent<Light>().color = color;

            carattere1.GetComponent<MeshRenderer>().material = newMaterial;
            carattere2.GetComponent<MeshRenderer>().material = newMaterial;
        }
    }
}
