using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBRangedAttack : EC_SpellObject
{
    bool hitSomething = false;
    bool hasDestination = false;
    Vector3 direction;

    float startingY;
    float timeTillDie = 3.0f;

    int damage = 20;
    int force = 15;

    float fireballSpeed = 2.0f;

    public GameObject collisionEffect;
    public ParticleSystem[] nonCollisionEffects;

    void OnEnable()
    {
        startingY = transform.position.y;
        collisionEffect.SetActive(false);
        Invoke("Destroy", 5.0f);
    }
    public override void ActivateSpell()
    {
        direction = FindObjectOfType<PC_PlayerVitals>().transform.position - transform.position;

        direction.y += 1.2f; /* Player is around 1 meter tall, this makes it so we dont shoot their feet */

        hasDestination = true;
    }

    void Update()
    {
        if (hasDestination)
        {

            transform.position += direction * fireballSpeed * Time.deltaTime;

            //Vector3 pos = new Vector3(transform.position.x, startingY, transform.position.z);

            //transform.position = pos;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PC_PlayerVitals>().HandleDamage(damage, force, null);

            TurnOffNonCollisionEffects();
            collisionEffect.SetActive(true);
            hasDestination = false;
            hitSomething = true;

            transform.parent = other.gameObject.transform;

            transform.position += direction * fireballSpeed * Time.deltaTime * 5;
            Invoke(nameof(Destroy), timeTillDie);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("WorldObject"))
        {
            TurnOffNonCollisionEffects();
            collisionEffect.SetActive(true);
            Invoke(nameof(Destroy), timeTillDie);
        }
    }

    void TurnOffNonCollisionEffects()
    {
        foreach (ParticleSystem particleSystem in nonCollisionEffects)
        {
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
