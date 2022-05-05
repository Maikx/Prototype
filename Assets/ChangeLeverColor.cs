using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLeverColor : MonoBehaviour
{
    public Color turnOnColor, turnOffColor;
    public Material turnOnMaterial, turnOffMaterial;
    public GameObject sphereLight;
    public GameObject pointlight;

    public void TurnOnLever()
    {
        pointlight.GetComponent<Light>().color = turnOnColor;
        sphereLight.GetComponent<MeshRenderer>().material = turnOnMaterial;
    }

    public void TurnOffLever()
    {
        pointlight.GetComponent<Light>().color = turnOffColor;
        sphereLight.GetComponent<MeshRenderer>().material = turnOffMaterial;
    }
}
