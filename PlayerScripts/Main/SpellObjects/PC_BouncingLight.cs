using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_BouncingLight : PC_SpellObject
{
    public int allowedBounces = 3;
    public float projectileSpeed = 2.0f;
    public float attackDetectionRadius = 15.0f;
    public float damage = 15.0f;
    public int force = 15;
    public LayerMask detectionLayer;

    int currBounces = 0;
    PC_PlayerVitals wielder;
    bool startMovement = false;
    Transform currTarget;
    List<EC_EnemyVitals> hitEnemies;
    float startingY;

    ParticleSystem[] particleSystems;

    public GameObject collisionParticles;

    public override void ActivateSpell()
    {
        particleSystems = GetComponents<ParticleSystem>();
        startingY = transform.position.y;
        startMovement = false;
        hitEnemies = new List<EC_EnemyVitals>();
        wielder = FindObjectOfType<PC_PlayerVitals>();
        CheckForMob();
        //Invoke("Destroy", 5.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(other.transform == currTarget)
            {
                startMovement = false;
                other.gameObject.GetComponent<EC_EnemyVitals>().HandleDamage(damage, force, wielder);
                currBounces++;

                GameObject clone = Instantiate(collisionParticles, transform.position, Quaternion.identity);

                /* play sound */
                CheckForMob();
            }
        }
    }

    void Update()
    {
        if (startMovement)
        {
            /* Find the direction towards the mob we found and go to hit till we hit it */
            if(currTarget)
            {
                Vector3 direction = currTarget.transform.position - transform.position;

                transform.position += direction * projectileSpeed * Time.deltaTime;

                Vector3 pos = new Vector3(transform.position.x, startingY, transform.position.z);

                transform.position = pos;
            }
        }
    }

    void CheckForMob()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackDetectionRadius, detectionLayer);

        float shortestDistance = attackDetectionRadius;

        currTarget = null;

        if(colliders.Length > 0 && currBounces < allowedBounces)
        {
            foreach (Collider collider in colliders)
            {
                float tempDistance = Vector3.Distance(transform.position, collider.transform.position);

                if(!hitEnemies.Contains(collider.GetComponent<EC_EnemyVitals>()))
                {
                    if (tempDistance < shortestDistance)
                    {
                        shortestDistance = tempDistance;
                        currTarget = collider.transform;
                    }
                }
            }

            if(currTarget == null)
            {
                FadeOut();
            }
            else
            {
                startMovement = true;
                hitEnemies.Add(currTarget.GetComponent<EC_EnemyVitals>());

            }

        }
        else
        {
            FadeOut(); /* Maybe do a fade out instead along with disabling it then destroying it on a timer */
        }

    }

    void FadeOut()
    {
        foreach(ParticleSystem ps in particleSystems)
        {
            ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        Invoke(nameof(Destroy), 10.0f);
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }

}
