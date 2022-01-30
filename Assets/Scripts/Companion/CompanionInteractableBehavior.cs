using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionInteractableBehavior : MonoBehaviour
{
    public bool ephemeralResettable;
    public bool levitable;
    public bool stoppable;
    public bool pressable;

    /// <summary>
    /// Using the bools this script will understand what the interactable can do..(it wont do it if the script isn't present)
    /// </summary>
    public void Interact()
    {
        if (ephemeralResettable)
            GetComponent<EphemeralWellBehavior>().Reset();

        if (levitable)
        {
            if(GetComponent<ObjectBehavior>().isLevitating == true)
                GetComponent<ObjectBehavior>().isLevitating = false;
            else
                GetComponent<ObjectBehavior>().isLevitating = true;
        }

        if (pressable)
        {
            if (GetComponent<ButtonBehavior>() != null)
                GetComponent<ButtonBehavior>().Pressed();
            else if (GetComponent<LeverBehavior>() != null)
            {
                if (GetComponent<LeverBehavior>().isActive == true)
                    GetComponent<LeverBehavior>().isActive = false;
                else
                    GetComponent<LeverBehavior>().isActive = true;
            }
        }
    }

    public void HeldInteract()
    {
        if(stoppable)
        {
            GetComponent<ObjectBehavior>().isStopped = true;
        }

        if(pressable && GetComponent<ButtonBehavior>() != null && GetComponent<ButtonBehavior>().type == ButtonBehavior.Type.ButtonB)
        {
            GetComponent<ButtonBehavior>().isActive = true;
        }
    }

    public void HeldInteractStop()
    {
        if (stoppable)
        {
            GetComponent<ObjectBehavior>().isStopped = false;
        }

        if (pressable && GetComponent<ButtonBehavior>() != null && GetComponent<ButtonBehavior>().type == ButtonBehavior.Type.ButtonB)
        {
            GetComponent<ButtonBehavior>().isActive = false;
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
