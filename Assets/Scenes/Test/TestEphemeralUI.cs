using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEphemeralUI : MonoBehaviour
{
    public GameObject ephemeral;
    public GameObject UI, vertUI;
    public GameObject oldUI;
    public GameObject newUI;

    public bool isUIInstantiate;
    
    void Update()
    {        
        if (ephemeral == null)
        {
            ephemeral = GameObject.FindWithTag("Ephemeral");
        }

        Vector2 horizontalUI = new Vector2(ephemeral.transform.position.x + 5, ephemeral.transform.position.y);
        Vector2 verticalUI = new Vector2(ephemeral.transform.position.x, ephemeral.transform.position.y + 3);
        Vector2 downVerticalUI = new Vector2(ephemeral.transform.position.x, ephemeral.transform.position.y - 3);

        if (Input.GetKeyDown(KeyCode.LeftControl) && !isUIInstantiate && newUI == null)
        {            
            newUI = Instantiate(UI, horizontalUI, Quaternion.identity);
            isUIInstantiate = true;
        }

        if (Input.GetKey(KeyCode.W) && !isUIInstantiate && newUI == null)
        {
            newUI = Instantiate(vertUI, verticalUI, Quaternion.Euler(0, 0, 90));
            isUIInstantiate = true;
        }

        if (Input.GetKey(KeyCode.S) && !isUIInstantiate && newUI == null)
        {
            newUI = Instantiate(vertUI, downVerticalUI, Quaternion.Euler(0, 0, -90));
            isUIInstantiate = true;
        }

        if (isUIInstantiate && newUI != null)
        {
            if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                Destroy(newUI);
                isUIInstantiate = false;
            }
        }        
    }
}
