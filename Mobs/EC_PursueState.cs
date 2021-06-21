using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EC_PursueState : EC_State
{
    public EC_CombatStanceState combatStanceState;

    public EC_Strafe strafeState;

    public EC_PursueState pursueState;

    public float checkIntervalTime = .5f;

    public override EC_State Tick(EC_EnemyManager enemyManager, EC_EnemyVitals enemyVitals, EC_AnimatorController animationManager)
    {
        /* Chase the target */
        /* IF within attack range, switch to combat stance state */
        /* If target is out of range, return this state and continue to pursue target */

        HandleRotateTowardsTarget(enemyManager);


        if (enemyManager.isPerformingAction)
        {
            animationManager.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            return this;
        }

        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        float viewableAngle = Vector3.AngleBetween(targetDirection, enemyManager.transform.forward);
        
        if (distanceFromTarget > enemyManager.maximumAttackRange)
        {
            enemyManager.animatorController.animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
        }

        HandleRotateTowardsTarget(enemyManager);

        if (distanceFromTarget <= enemyManager.maximumAttackRange)
        {
            return combatStanceState;
        }
        else if (distanceFromTarget <= enemyManager.maximumRangedAttackRange && !enemyManager.spellOnCD && enemyManager.isSpellCaster)
        {
            return combatStanceState;
        }
        else
        {
            return this;
        }

    }

    private void HandleRotateTowardsTarget(EC_EnemyManager enemyManager)
    {
        /* Rotate enemy manually to perform actions against player */
        if (enemyManager.isPerformingAction)
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            direction.y = 0; /* here check to see if they are grounded ? */
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = enemyManager.transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
        /* Rotate by nav mesh pathfinding */
        else
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);

            Vector3 targetVelocity = enemyManager.rigidbody.velocity;

            enemyManager.navMeshAgent.enabled = true;

            enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.rigidbody.velocity = targetVelocity;
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
            //CheckForward(enemyManager);
        }


    }

    /* Used to realign the AI if it is running into another AI or a wall */
    private IEnumerator CheckForward(EC_EnemyManager enemyManager)
    {
        if ((enemyManager.navMeshAgent.pathStatus == NavMeshPathStatus.PathPartial))

        {

            Vector3 newPos = RandomNavSphere(transform.position, 1f, 8);

            if (enemyManager.navMeshAgent.enabled == true)

                enemyManager.navMeshAgent.SetDestination(newPos);

        }

        yield return new WaitForSeconds(checkIntervalTime);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)

    {

        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;

    }
}
