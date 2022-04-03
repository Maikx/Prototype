using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxBehavior : MonoBehaviour
{
    private float lenght, startPosX, startPosY;
    public GameObject cam;
    public float parallaxEffectX, parallaxEffectY;

    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffectX));
        float dist = (cam.transform.position.x * parallaxEffectX);
        float up = (cam.transform.position.y * parallaxEffectY);

        transform.position = new Vector3(startPosX + dist, startPosY + up, transform.position.z);

        if (temp > startPosX + lenght)
        {
            startPosX += lenght;
        }
        else if (temp < startPosX - lenght)
        {
            startPosX -= lenght;
        }
    }
}
