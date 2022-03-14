using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentEffect : MonoBehaviour
{
    public GameObject[] Objects;

    private void Start()
    {
        for (int i = 0; i < Objects.Length; i++)
        {
            Objects[i].GetComponent<MeshRenderer>().material.renderQueue = 3002;
        }
    }
}
