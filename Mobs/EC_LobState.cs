using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_LobState : EC_State
{
    //public EC_IdleState idleState;
    public EC_CombatStanceState combatStanceState;
    public EC_EnemyVitals ev;
    public EC_EnemyManager em;
    EC_AnimatorController animatorController;
    public EC_EnemyAttackAction currentAttack;
    public GameObject lobAttack;
    public float attackRange = 20f;
    bool alreadyAttacked = false;
    private float cooldown = 5f;
    private float numAttacks = 3f;
    private float delay = 1.5f;
    bool isAttacking = false;

    // Start is called before the first frame update

    public override EC_State Tick(EC_EnemyManager enemyManager, EC_EnemyVitals enemyVitals, EC_AnimatorController animationManager)
    {
        if(enemyManager.currentTarget == null)
        {
            enemyManager.currentTarget = FindObjectOfType<PC_PlayerVitals>();
        }


        em = enemyManager;
        ev = enemyVitals;
        animatorController = animationManager;
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);
        //if (enemyManager.isPerformingAction) return combatStanceState;

        enemyManager.transform.LookAt(enemyManager.currentTarget.transform);
        
        if (distanceFromTarget < attackRange)
        {
            if (!alreadyAttacked)
            {
                alreadyAttacked = true;
                StartCoroutine("BarrageAttack");
            }

            return this;
        }
        else
        {
            if (isAttacking)
            {
                alreadyAttacked = false;
                StopCoroutine("BarrageAttack");

            }
            return this;
        }
    }


    //// Update is called once per frame
    //private void HandleRotateTowardsTarget(EC_EnemyManager enemyManager)
    //{
    //    /* Rotate enemy manually to perform actions against player */
    //    if (enemyManager.isPerformingAction)
    //    {
    //        Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
    //        direction.y = 0;
    //        direction.Normalize();

    //        if (direction == Vector3.zero)
    //        {
    //            direction = enemyManager.transform.forward;
    //        }

    //        Quaternion targetRotation = Quaternion.LookRotation(direction);
    //        enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
    //    }
    //    /* Rotate by nav mesh pathfinding */
    //    else
    //    {
    //        Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
    //        Vector3 targetVelocity = enemyManager.rigidbody.velocity;

    //        enemyManager.navMeshAgent.enabled = true;

    //        enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
    //        enemyManager.rigidbody.velocity = targetVelocity;
    //        enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
    //    }


    //}
    IEnumerator BarrageAttack()
    {
        isAttacking = true;
        for (int i = 0; i < numAttacks; i++)
        {
            animatorController.PlayTargetAnimation("UpwardCast", true, 1);
            SpawnRangedAttack();
            yield return new WaitForSeconds(delay);
        }
        Invoke("ResetAttack", cooldown);
        isAttacking = false;
        yield return null;
    }
    public void SpawnRangedAttack() /* Called from animation */
    {
        /* prefab spawn and send at player with specfic speed */

        GameObject rangedAttack = Instantiate(lobAttack, transform.position, Quaternion.identity);

        rangedAttack.GetComponent<MonsterERange>().BeginTravel(transform.position, em.currentTarget.transform.position, 15f, ev);


    }
    protected void ResetAttack()
    {
        alreadyAttacked = false;
        //agent.isStopped = false;
    }
}
