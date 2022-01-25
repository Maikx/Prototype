using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehavior : MonoBehaviour
{
    public GameObject[] linkedObjects;
    [HideInInspector]public bool isActive;

    private void Update()
    {
        Lever();
    }

    void Lever()
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
