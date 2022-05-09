using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarkInteraction : MonoBehaviour
{
    [HideInInspector] private PlayerController pC;
    [HideInInspector] public GameObject barkTrigger;
    [HideInInspector] private CapsuleDirection2D directionVertical;
    [HideInInspector] private CapsuleDirection2D directionHorizontal;

    public float barkTriggerDuration = 0.5f;
    
    //0 = none, 1 = right, 2 = left, 3 = up, 4 = down..
    [HideInInspector]public int lastDirection;

    private void Start()
    {
        pC = gameObject.GetComponent<PlayerController>();
    }

    public void BarkLeft()
    {
        lastDirection = 2;
        barkTrigger.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 1);
        barkTrigger.GetComponent<CapsuleCollider2D>().size = new Vector2(0.5f, 2);
        barkTrigger.GetComponent<CapsuleCollider2D>().direction = directionVertical;
        barkTrigger.GetComponent<CapsuleCollider2D>().enabled = true;
        StartCoroutine(waitForSec(barkTriggerDuration));
    }

    public void BarkRight()
    {
        lastDirection = 1;
        barkTrigger.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 1);
        barkTrigger.GetComponent<CapsuleCollider2D>().size = new Vector2(0.5f, 2);
        barkTrigger.GetComponent<CapsuleCollider2D>().direction = directionVertical;
        barkTrigger.GetComponent<CapsuleCollider2D>().enabled = true;
        StartCoroutine(waitForSec(barkTriggerDuration));
    }

    public void BarkUp()
    {
        lastDirection = 3;
        barkTrigger.GetComponent<CapsuleCollider2D>().offset = new Vector2(-1, 0);
        barkTrigger.GetComponent<CapsuleCollider2D>().size = new Vector2(2, 1f);
        barkTrigger.GetComponent<CapsuleCollider2D>().direction = directionHorizontal;
        barkTrigger.GetComponent<CapsuleCollider2D>().enabled = true;
        StartCoroutine(waitForSec(barkTriggerDuration));
    }

    public void BarkDown()
    {
        lastDirection = 4;
        barkTrigger.GetComponent<CapsuleCollider2D>().offset = new Vector2(1, 0);
        barkTrigger.GetComponent<CapsuleCollider2D>().size = new Vector2(2, 1f);
        barkTrigger.GetComponent<CapsuleCollider2D>().direction = directionHorizontal;
        barkTrigger.GetComponent<CapsuleCollider2D>().enabled = true;
        StartCoroutine(waitForSec(barkTriggerDuration));
    }

    private IEnumerator waitForSec(float sec)
    {
        pC.canMoveBark = false;
        yield return new WaitForSeconds(sec);
        barkTrigger.GetComponent<CapsuleCollider2D>().enabled = false;
        lastDirection = 0;
        pC.canMoveBark = true;
    }
}
