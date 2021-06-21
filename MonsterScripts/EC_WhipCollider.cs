using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_WhipCollider : PC_EC_MeleeCollider
{
    Collider[] damageColliders;

    public bool activated;

    private void Awake()
    {
        if(GetComponentInParent<PC_PlayerManager>())
        {
            myOpponent = "Enemy";
        }
        else
        {
            myOpponent = "Player";
        }
        damageColliders = GetComponentsInChildren<Collider>();

        foreach(Collider collider in damageColliders)
        {
            collider.gameObject.SetActive(true);
            collider.isTrigger = true;
            collider.enabled = false;
        }

        activated = false;
        wielder = GetComponentInParent<PC_EC_Vitals>();
    }


    public override void EnableDamageCollider()
    {
        foreach (Collider collider in damageColliders)
        {
            collider.enabled = true;

        }
        if (!activated)
        {

            activated = true;
        }

    }

    public override void DisableDamageCollider()
    {
        foreach (Collider collider in damageColliders)
        {
            collider.enabled = false;

            if (activated)
        {

            }
            activated = false;
        }

    }



}
