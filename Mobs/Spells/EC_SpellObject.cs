using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script is attached to prefabs which are placed underneath enemy attack actions */
/* When an enemy attacks, it will check if the action has a spellobject and then the animator will call 
 * the current spell object to activate at a specific frame if the action has one */

public abstract class EC_SpellObject : MonoBehaviour
{
    public virtual void ActivateSpell()
    {

    }
}
