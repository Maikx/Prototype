using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightColor : MonoBehaviour
{
    public Color color;
    public GameObject spotlight;
    public Animator totemAnimator;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Ephemeral")
        {
            totemAnimator.SetTrigger("TotemUp");
            spotlight.GetComponent<Light>().color = color;
        }
    }
}
