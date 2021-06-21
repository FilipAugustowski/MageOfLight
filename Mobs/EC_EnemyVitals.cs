using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_EnemyVitals : PC_EC_Vitals
{
    EC_EnemyManager enemyManager;
    EC_AnimatorController animatorController;
    public EC_State damageState;
    public bool isStationary;

    public bool isDummy = false;
    public int timesHit = 0;
    public int dummyHits = 3;

    public GameObject bloodSpray;
    Vector3 spraySpawnPos;

    protected override void Init()
    {
        animatorController = GetComponentInChildren<EC_AnimatorController>();
        enemyManager = GetComponent<EC_EnemyManager>();
        spraySpawnPos = new Vector3(0, 1.6f, 0);
    }

    public void HandleRoot(float _rootTime)
    {
        animatorController.DeactivateMeleeColliderLeft();
        animatorController.DeactivateMeleeColliderRight();

        if (!enemyManager.isDead)
        {
            enemyManager.isRooted = true;
            enemyManager.rootTime = _rootTime;
            enemyManager.SwitchToNextState(damageState);
            enemyManager.animatorController.PlayTargetAnimation("Mob_Root", true, 1);

        }
    }

    public override void HandleDamage(float _damage, int _force, PC_EC_Vitals _whoDealtDamageToMeLast, bool _playSound = true)
    {
        base.HandleDamage(_damage, _force, _whoDealtDamageToMeLast, _playSound) ;

        animatorController.DeactivateMeleeColliderLeft();
        animatorController.DeactivateMeleeColliderRight();


        GameObject clone = GameObject.Instantiate(bloodSpray, this.transform.position + spraySpawnPos, Quaternion.identity);
        clone.transform.parent = transform;

        if(isDummy)
        {
            timesHit++;
            if(timesHit >= dummyHits)
            {
                enemyManager.isDead = true;
            }
        }

        if(!enemyManager.isDead)
        {

            enemyManager.SwitchToNextState(damageState);
            if (health <= 0)
            {
                currentKnockback = 0;
                animatorController.PlayTargetAnimation("Death", true, 1);
                enemyManager.GetComponent<Collider>().enabled = false;
                Collider[] colliders = enemyManager.GetComponentsInChildren<Collider>();

                foreach(Collider collider in colliders)
                {
                    collider.enabled = false;
                }

                enemyManager.GetComponentInChildren<PC_CharacterColliderBlocker>().enabled = false;
                enemyManager.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

                if(LvlManager.Instance && !isDummy)
                {
                    LvlManager.Instance.SignalMonsterDeath(enemyManager.mobID, enemyManager.isBoss);

                    if (enemyManager.isBoss) AudioManager.Instance.Play("Level_4_AfterBoss");
                }
                if(LightManager.Instance)
                {
                    if(!isDummy && enemyManager.mobID >= 0) LightManager.Instance.Illuminate(enemyManager.mobID, this.transform.position);
                }
            }
            else if (currentKnockback >= heavyHitForceReqForAnim)
            {
                currentKnockback = 0;
                if (!animatorController.animator.GetCurrentAnimatorStateInfo(1).IsName("Hit_Heavy"))
                {
                    animatorController.PlayTargetAnimation("Hit_Heavy", false, 1);

                }
            }
            else if (currentKnockback >= mediumHitForceReqForAnim)
            {
                if (animatorController.lastStatePlayedByTargeting != "Hit_Heavy")
                {
                    animatorController.PlayTargetAnimation("Hit_Medium", false, 1);

                }

            }
            else if (currentKnockback >= lightHitForceReqForAnim)
            {
                if(animatorController.lastStatePlayedByTargeting != "Hit_Medium" &&
                    animatorController.lastStatePlayedByTargeting != "Hit_Heavy")
                {
                    animatorController.PlayTargetAnimation("Hit_Light", false, 1);

                }


            }

        }
        else if(animatorController.lastStatePlayedByTargeting != "Death")
        {
            enemyManager.SwitchToNextState(damageState);

            currentKnockback = 0;
            animatorController.PlayTargetAnimation("Death", true, 1);
            enemyManager.GetComponent<Collider>().enabled = false;
            Collider[] colliders = enemyManager.GetComponentsInChildren<Collider>();

            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }

            enemyManager.GetComponentInChildren<PC_CharacterColliderBlocker>().enabled = false;
            enemyManager.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            if (LvlManager.Instance && !isDummy) LvlManager.Instance.SignalMonsterDeath(enemyManager.mobID, enemyManager.isBoss);
            if (LightManager.Instance) if (!isDummy && enemyManager.mobID >= 0) LightManager.Instance.Illuminate(enemyManager.mobID, this.transform.position);
        }

    }
}
