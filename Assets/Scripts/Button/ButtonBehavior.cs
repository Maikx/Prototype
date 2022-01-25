using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public enum Type {ButtonA, ButtonB};
    public Type type;

    public GameObject[] linkedObjects;

    [HideInInspector]public bool isActive;

    [Header("ButtonA Settings")]
    public float timer;
    float currentTimer;

    private void Update()
    {
        Timer();
        Button();
    }

    void Timer()
    {
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }
        else if (currentTimer <= 0 && type == Type.ButtonA)
        {
            isActive = false;
        }
    }

    public void Pressed()
    {
        if(type == Type.ButtonA)
        {
                currentTimer = timer;
                isActive = true;
        }
    }

    void Button()
    {
        if (isActive)
        {
            for (int i = 0; i < linkedObjects.Length; i++)
            {
                if (linkedObjects[i].GetComponent<PlatformBehavior>() != null)
                {
                    linkedObjects[i].GetComponent<PlatformBehavior>().isMoving = true;
                }
            }
        }
        else if (!isActive)
        {
            for (int i = 0; i < linkedObjects.Length; i++)
            {
                if (linkedObjects[i].GetComponent<PlatformBehavior>() != null)
                {
                    linkedObjects[i].GetComponent<PlatformBehavior>().isMoving = false;
                }
            }
        }
    }
}
