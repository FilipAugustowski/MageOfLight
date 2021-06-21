using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCDemonAttack : EC_SpellObject
{
    bool hitSomething = false;
    bool hasDestination = false;

    float timeTillDie = 3.0f;

    int damage = 25;
    float hitForce = 6.0f;

    float fireballSpeed = 10.0f;

    //public GameObject collisionEffect;
    public GameObject nonCollisionEffects;

    Vector3 direction;

    void OnEnable()
    {
        //collisionEffect.SetActive(false);


        Invoke("Destroy", 5.0f);
    }

    public override void ActivateSpell()
    {
        hasDestination = true;
        //direction = _direction;

        //foreach (RFX4_ParticleTrail particle in nonCollisionEffects.GetComponentsInChildren<RFX4_ParticleTrail>())
        //{
        //    particle.SetTarget(GameObject.FindGameObjectWithTag("Player"));
        //}
        nonCollisionEffects.SetActive(true); /* These are always intially off */
        nonCollisionEffects.transform.LookAt(PC_PlayerManager.Instance.transform); /* was PlayerTracker.Instance.gameObject.transform.position */
    }

    void Update()
    {
        //if (hasDestination)
        //{
        //    //transform.position += direction * fireballSpeed * Time.deltaTime;
        //    nonCollisionEffects.transform.LookAt(PC_PlayerManager.Instance.transform);
        //}
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Invoke(nameof(Destroy), timeTillDie);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("WorldObject"))
        {
            TurnOffNonCollisionEffects();
            //collisionEffect.SetActive(true);
            Invoke(nameof(Destroy), timeTillDie);
        }
    }

    void TurnOffNonCollisionEffects()
    {
        foreach (ParticleSystem particleSystem in nonCollisionEffects.GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.Pause();
        }
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
