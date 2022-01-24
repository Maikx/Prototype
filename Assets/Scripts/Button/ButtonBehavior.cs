using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public enum Type { None, Button1, Button2};
    public Type type;

    [Header("Button2 Settings")]
    public float timer;
    float currentTimer;

    private void Update()
    {
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }
    }

    public void Pressed()
    {
        if(type == Type.Button1)
        {
            Debug.Log("Pressed Type1");
        }
        else if(type == Type.Button2)
        {
            currentTimer = timer;
            Debug.Log("Pressed Type2");
        }
    }
}
