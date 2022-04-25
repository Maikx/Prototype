using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBehavior : MonoBehaviour
{
    public GameObject planks;
    public GameObject ephemeral;
    public GameObject[] rope;

    [Header("Ephemeral")]
    public float fallSpeed;
    public float fallHeight;
    private Vector3 fallLocation;
    private bool isFalling;

    private void Update()
    {
        if (ephemeral != null && isFalling)
            MakeEphemeralFall();
    }

    public void MakePlanksFall()
    {
        Destroy(planks);
        isFalling = true;
        for (int i = 0; i < rope.Length; i++)
            Destroy(rope[i]);
    }

    public void MakeEphemeralFall()
    {
        ephemeral.transform.position = Vector3.MoveTowards(ephemeral.transform.position, new Vector3(ephemeral.transform.position.x, fallHeight, ephemeral.transform.position.z), fallSpeed * Time.deltaTime);
        if (ephemeral.transform.position == fallLocation) isFalling = false;
    }

}
