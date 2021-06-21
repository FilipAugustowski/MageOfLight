using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_EC_MeleeCollider : MonoBehaviour
{
    Collider damageCollider;
    public string myOpponent;

    public int currForce;
    public float currDamage;

    public PC_EC_Vitals wielder;

    bool isPlayerWeapon = false;

    public string[] swordDamageSounds;

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
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
        wielder = GetComponentInParent<PC_EC_Vitals>();

        if (wielder.GetType() == typeof(PC_PlayerVitals)) isPlayerWeapon = true;

    }

    public void UpdateDamage(float _input)
    {
        currDamage = _input; 
    }

    public void UpdateForce(int _input)
    {
        currForce = _input;
    }

    public virtual void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }

    public virtual void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == myOpponent)
        {
            if(isPlayerWeapon && swordDamageSounds.Length > 0)
            {
                int x = swordDamageSounds.Length;

                int random = Random.Range(0, x);

                AudioManager.Instance.Play(swordDamageSounds[random]);
            }


            /* Call take damage on the damage handler of either the player or the AI */
            other.gameObject.GetComponent<PC_EC_Vitals>().HandleDamage(currDamage, currForce, wielder);
        }
    }

}
