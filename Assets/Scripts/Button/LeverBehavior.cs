using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehavior : MonoBehaviour
{
    public Animator animatorLever;    

    public GameObject[] linkedObjects;
    [HideInInspector]public bool isActive;

    public GameObject platformLight;

    [Header("Materials")]
    public Material onMaterial;
    public Material offMaterial;


    private void Update()
    {
        Lever();
    }

    void Lever()
    {
        if (isActive)
        {
            platformLight.SetActive(true);
            animatorLever.SetBool("IsTurnedOn", true);            
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
            platformLight.SetActive(false);
            animatorLever.SetBool("IsTurnedOn", false);
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
