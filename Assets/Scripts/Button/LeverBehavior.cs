using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehavior : MonoBehaviour
{
    public Animator animatorLever;    

    public GameObject[] linkedObjects;
    public GameObject[] linkedObjectsMaterial;
    [HideInInspector]public bool isActive;

    private void Update()
    {
        Lever();
    }

    void Lever()
    {
        if (isActive)
        {
            animatorLever.SetBool("IsTurnedOn", true);            
            for (int i = 0; i < linkedObjects.Length; i++)
            {
                if (linkedObjects[i].GetComponent<PlatformBehavior>() != null)
                {
                    linkedObjects[i].GetComponent<PlatformBehavior>().isMoving = true;
                    linkedObjectsMaterial[i].GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                }
            }
        }
        else if (!isActive)
        {
            animatorLever.SetBool("IsTurnedOn", false);
            for (int i = 0; i < linkedObjects.Length; i++)
            {
                if (linkedObjects[i].GetComponent<PlatformBehavior>() != null)
                {
                    linkedObjects[i].GetComponent<PlatformBehavior>().isMoving = false;
                    linkedObjectsMaterial[i].GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                }
            }
        }
    }    
}
