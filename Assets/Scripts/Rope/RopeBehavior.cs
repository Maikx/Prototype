using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBehavior : MonoBehaviour
{
    public GameObject planks;


    public void MakePlanksFall()
    {
        Destroy(planks);
    }

}
