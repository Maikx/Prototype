using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLamps : MonoBehaviour
{
    public GameObject lampPrefab;
    public GameObject oldLamp;
    public GameObject newLamp;
    private GameObject companion;
    private float lastClickTime;
    private const float doubleClickTime = .4f;

    void SpawnLamp()
    {        
        if (oldLamp == null && newLamp != null)
        {
            oldLamp = newLamp;
            newLamp = null;
        }

        if (Input.GetMouseButtonDown(1) && newLamp == null)
        {
            Destroy(oldLamp);
            oldLamp = null;
            float timeSinceLastClick = Time.time - lastClickTime;

            if (timeSinceLastClick <= doubleClickTime)
            {
                newLamp = Instantiate(lampPrefab, companion.gameObject.transform.position, Quaternion.identity);
            }

            lastClickTime = Time.time;
        }
                
    }
    
    void Update()
    {
        if(companion == null)
            companion = GameObject.FindWithTag("Companion");

        SpawnLamp();
    }
}
