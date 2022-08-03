using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightIntensity : MonoBehaviour
{
    public LightManager lightManager;

    private void Start()
    {
        lightManager = lightManager.GetComponent<LightManager>();
    }

    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
            lightManager.changeIntensity = true;
    }

    public void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == "Player")
            lightManager.changeIntensity = false;
    }
}
