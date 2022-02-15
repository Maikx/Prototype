using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBehavior : MonoBehaviour
{
    public enum Target { Player, Companion, Both };
    public Target target;

    public GameObject[] linkedObjects;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target == Target.Player && collision.gameObject.layer == 9)
        {
            Debug.Log("PlayerTrigger");
            for (int i = 0; i < linkedObjects.Length; i++)
            {
                if (linkedObjects[i].GetComponent<ObjectBehavior>() != null)
                { 
                    linkedObjects[i].GetComponent<ObjectBehavior>().Activate();
                }
            }
        }
        else if (target == Target.Companion && collision.gameObject.layer == 10)
        {
            Debug.Log("CompanionTrigger");
            for (int i = 0; i < linkedObjects.Length; i++)
            {
                if (linkedObjects[i].GetComponent<ObjectBehavior>() != null)
                {
                    linkedObjects[i].GetComponent<ObjectBehavior>().Activate();
                }
            }
        }
        else if (target == Target.Both && collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        {
            Debug.Log("Both");
            for (int i = 0; i < linkedObjects.Length; i++)
            {
                if (linkedObjects[i].GetComponent<ObjectBehavior>() != null)
                {
                    linkedObjects[i].GetComponent<ObjectBehavior>().Activate();
                }
            }
        }
    }
}
