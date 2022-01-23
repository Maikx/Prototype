using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionInteractableBehavior : MonoBehaviour
{
    public bool ephemeralReset;
    public bool levitation;
    public bool stoppable;

    /// <summary>
    /// Using the bools this script will understand what the interactable can do..(it wont do it if the script isn't present)
    /// </summary>
    public void Interact()
    {
        if (ephemeralReset)
            GetComponent<EphemeralWellBehavior>().Reset();

        if (levitation)
        {
            if(GetComponent<ObjectBehavior>().isLevitating == true)
                GetComponent<ObjectBehavior>().isLevitating = false;
            else
                GetComponent<ObjectBehavior>().isLevitating = true;
        }
    }

    public void HeldInteract()
    {
        if(stoppable)
        {
            GetComponent<ObjectBehavior>().isStopped = true;
        }
    }

    public void HeldInteractStop()
    {
        if (stoppable)
        {
            GetComponent<ObjectBehavior>().isStopped = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            HeldInteractStop();
        }
    }
}
