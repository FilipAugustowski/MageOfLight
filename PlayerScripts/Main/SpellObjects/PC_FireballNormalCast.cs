using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_FireballNormalCast : PC_SpellObject
{
    bool hitSomthing = false;
    bool hasDestination = false;
    Vector3 direction;

    float timeTillDie = 3.0f;

    public int damage = 30;
    public int force = 30;

    float fireballSpeed = 10.0f;

    public GameObject collisionEffect;
    public ParticleSystem[] nonCollisionEffects;

    PC_PlayerVitals wielder;

    public override void ActivateSpell()
    {
        wielder = FindObjectOfType<PC_PlayerVitals>();
        collisionEffect.SetActive(false);
        Invoke("Destroy", 5.0f);
        BeginTravel(Camera.main.transform.forward);
    }

    public void BeginTravel(Vector3 _direction)
    {
        direction = _direction;
        hasDestination = true;
    }

    void Update()
    {
        if (hasDestination)
        {
            transform.position += direction * fireballSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!hitSomthing)
            {
                other.gameObject.GetComponent<EC_EnemyVitals>().HandleDamage(damage, force, wielder);
                //if (other.gameObject.GetComponent<MonsterAI>().CheckIfRooted())
                //{
                //    other.gameObject.GetComponent<MonsterAI>().TakeDamage(damage, hitForce, true);

                //}
                //else
                //{
                //    other.gameObject.GetComponent<MonsterAI>().TakeDamage(damage, hitForce);

                //}
                hitSomthing = true;
            }
            TurnOffNonCollisionEffects();
            collisionEffect.SetActive(true);
            hasDestination = false;


            Invoke(nameof(Destroy), timeTillDie);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("WorldObject"))
        {
            hasDestination = false;
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
