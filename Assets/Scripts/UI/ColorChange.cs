using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{
    public Color myColor;
    public Image image;

    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        image.color = myColor;
    }
}
