using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public enum Type { None, Button1, Button2};
    public Type type;

    public GameObject[] linkedObjects;

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
            for (int i = 0; i < linkedObjects.Length; i++)
            {
               if(linkedObjects[i].GetComponent<PlatformBehavior>() != null)
                {
                    linkedObjects[i].GetComponent<PlatformBehavior>().isMoving = true;
                }
            }
        }
        else if(type == Type.Button2)
        {
            currentTimer = timer;
            Debug.Log("Pressed Type2");
        }
    }
}
