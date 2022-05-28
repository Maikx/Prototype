using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Light directionalLight;
    public bool changeIntensity;
    public float intensitySpeed;
    public float darkIntensity;
    public float lightIntensity;

    public void Update()
    {
        DarkZone();
        LightZone();
    }

    void DarkZone()
    {
        if (changeIntensity && directionalLight.intensity >= darkIntensity)
        {
            directionalLight.intensity -= intensitySpeed * Time.deltaTime;
        }
    }

    void LightZone()
    {
        if (!changeIntensity && directionalLight.intensity <= lightIntensity)
        {
            directionalLight.intensity += intensitySpeed * Time.deltaTime;
        }
    }
}