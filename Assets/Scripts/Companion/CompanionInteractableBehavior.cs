using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionInteractableBehavior : MonoBehaviour
{
    public bool ephemeralReset;
    public bool levitation;

    /// <summary>
    /// Using the bools this script will understand what the interactable can do..(it wont do it if the script isn't present)
    /// </summary>
    public void Interact()
    {
        if (ephemeralReset)
            GetComponent<EphemeralWellBehavior>().Reset();

        if (levitation)
            GetComponent<ObjectBehavior>();
    }
}
