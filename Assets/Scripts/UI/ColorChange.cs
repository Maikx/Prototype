using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{
    public Color myColor;
    public Image image;

    public void OnTriggerEnter2D()
    {
        image.color = myColor;
    }
}
