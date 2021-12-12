using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarkInteraction : MonoBehaviour
{
    [HideInInspector] public GameObject barkTrigger;
    [HideInInspector] public CapsuleDirection2D directionVertical;
    [HideInInspector] public CapsuleDirection2D directionHorizontal;

    public void BarkLeft()
    {
        barkTrigger.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 1);
        barkTrigger.GetComponent<CapsuleCollider2D>().size = new Vector2(0.5f, 2);
        barkTrigger.GetComponent<CapsuleCollider2D>().direction = directionVertical;
        barkTrigger.GetComponent<CapsuleCollider2D>().enabled = true;
        Debug.Log("BarkLeft");
        StartCoroutine(waitForSec(.5f));
    }

    public void BarkRight()
    {
        barkTrigger.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 1);
        barkTrigger.GetComponent<CapsuleCollider2D>().size = new Vector2(0.5f, 2);
        barkTrigger.GetComponent<CapsuleCollider2D>().direction = directionVertical;
        barkTrigger.GetComponent<CapsuleCollider2D>().enabled = true;
        Debug.Log("BarkRight");
        StartCoroutine(waitForSec(.5f));
    }

    public void BarkUp()
    {
        barkTrigger.GetComponent<CapsuleCollider2D>().offset = new Vector2(-1, 0);
        barkTrigger.GetComponent<CapsuleCollider2D>().size = new Vector2(2, 0.5f);
        barkTrigger.GetComponent<CapsuleCollider2D>().direction = directionHorizontal;
        barkTrigger.GetComponent<CapsuleCollider2D>().enabled = true;
        Debug.Log("BarkUp");
        StartCoroutine(waitForSec(.5f));
    }

    public void BarkDown()
    {
        barkTrigger.GetComponent<CapsuleCollider2D>().offset = new Vector2(1, 0);
        barkTrigger.GetComponent<CapsuleCollider2D>().size = new Vector2(2, 0.5f);
        barkTrigger.GetComponent<CapsuleCollider2D>().direction = directionHorizontal;
        barkTrigger.GetComponent<CapsuleCollider2D>().enabled = true;
        Debug.Log("BarkDown");
        StartCoroutine(waitForSec(.5f));
    }

    private IEnumerator waitForSec(float sec)
    {
        yield return new WaitForSeconds(sec);
        barkTrigger.GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
