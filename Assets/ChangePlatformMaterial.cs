using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlatformMaterial : MonoBehaviour
{
    public GameObject[] platform;
    public bool isCompanionIn, isPlatformLightUp;

    [Header("Materials")]
    public Material onMaterial;
    public Material offMaterial;

    public void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Companion")
        {
            isCompanionIn = true;
        }
    }

    public void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == "Companion")
        {
            isCompanionIn = false;
        }
    }

    public void Update()
    {
        if(isCompanionIn && !isPlatformLightUp && Input.GetMouseButtonDown(0))
        {
            isPlatformLightUp = true;

            for (int i = 0; i < platform.Length; i++)
            {
                platform[i].GetComponent<MeshRenderer>().material = onMaterial;
            }
        }
        
        if(isCompanionIn && isPlatformLightUp && Input.GetMouseButtonDown(0))
        {
            isPlatformLightUp = false;

            for (int i = 0; i < platform.Length; i++)
            {
                platform[i].GetComponent<MeshRenderer>().material = offMaterial;
            }
        }

    }
}
