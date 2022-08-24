using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ChangeLeverColor : MonoBehaviour
{
    public Color turnOnColor, turnOffColor;
    public Material turnOnMaterial, turnOffMaterial;
    public GameObject sphereLight;
    public GameObject pointlight;

    public void TurnOnLever()
    {
        pointlight.GetComponent<Light2D>().color = turnOnColor;
        sphereLight.GetComponent<MeshRenderer>().material = turnOnMaterial;
    }

    public void TurnOffLever()
    {
        pointlight.GetComponent<Light2D>().color = turnOffColor;
        sphereLight.GetComponent<MeshRenderer>().material = turnOffMaterial;
    }
}
