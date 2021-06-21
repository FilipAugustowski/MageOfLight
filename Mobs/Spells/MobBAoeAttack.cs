using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBAoeAttack : EC_SpellObject
{ 


    void OnEnable()
    {
        Invoke(nameof(Destroy), 6.0f);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PC_PlayerVitals>().HandleDamage(5 * Time.deltaTime, 0, null, false); 
        }
    }

    void Destroy()
    {
        GetComponentInChildren<RFX4_EffectSettings>().IsVisible = false;
        Destroy(this.gameObject);
    }
}
