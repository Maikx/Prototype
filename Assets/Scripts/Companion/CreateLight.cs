using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLight : MonoBehaviour
{
    public GameObject lightToInstantiate;
    public GameObject clone;
    public bool isInstantiate;
    public float timeToDestroy;
    private float lastClickTime;
    private const float doubleClickTime = .4f;

    void Update()
    {
        Vector3 spawnLight = new Vector3(transform.position.x, transform.position.y, transform.position.z - 8);

        if(Input.GetMouseButtonDown(1) && !isInstantiate)
        {
            float timeSinceLastClick = Time.time - lastClickTime;
            
            if(timeSinceLastClick <= doubleClickTime)
            {
                isInstantiate = true;
                GameObject cloneCompanion = Instantiate(clone, transform.position, Quaternion.identity);
                Destroy(cloneCompanion, timeToDestroy);
                GameObject cloneLight = Instantiate(lightToInstantiate, spawnLight, Quaternion.identity);
                Destroy(cloneLight, timeToDestroy);
                StartCoroutine(BoolChange());
            }

            lastClickTime = Time.time;
        }
    }

    IEnumerator BoolChange()
    {
        yield return new WaitForSeconds(timeToDestroy);
        isInstantiate = false;
        StopCoroutine(BoolChange());
    }
}
