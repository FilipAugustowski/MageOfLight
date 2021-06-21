using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonCollider : MonoBehaviour
{
    Collider collider;
    bool hasHit;
    public int myDamage;
    public int myForce;
    //public Transform myOrigin;

    void OnEnable()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
        hasHit = false;
        Invoke(nameof(EnableCollider), 1.3f);
        Invoke(nameof(Disable), 1.5f);
        Invoke(nameof(Die), 6.0f);
    }

    void EnableCollider()
    {
        collider.enabled = true;
    }



    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !hasHit)
        {
            hasHit = true;
            other.gameObject.GetComponent<PC_PlayerVitals>().HandleDamage(myDamage, myForce, null);
        }
    }
    void Disable()
    {
        hasHit = true;
    }

    void Die()
    {
        Destroy(this);
    }
}
