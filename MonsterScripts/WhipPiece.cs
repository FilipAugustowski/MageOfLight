using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipPiece : MonoBehaviour
{
    string myOpponent;
    PC_EC_Vitals wielder;
    EC_WhipCollider whip;

    void Start()
    {
        myOpponent = GetComponentInParent<PC_EC_MeleeCollider>().myOpponent;
        wielder = GetComponentInParent<PC_EC_MeleeCollider>().wielder;
        whip = GetComponentInParent<EC_WhipCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == myOpponent)
        {
            /* Call take damage on the damage handler of either the player or the AI */
            other.gameObject.GetComponent<PC_EC_Vitals>().HandleDamage(15, 15, wielder); /* Player doesnt need a reference to hit themm, at least for now */
            whip.DisableDamageCollider();
        }
    }
}
