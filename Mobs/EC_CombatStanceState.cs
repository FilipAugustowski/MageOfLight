using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_CombatStanceState : EC_State
{
    public EC_AttackState attackState;
    public EC_PursueState pursueState;
    public override EC_State Tick(EC_EnemyManager enemyManager, EC_EnemyVitals enemyVitals, EC_AnimatorController animationManager)
    {

        if(enemyManager.currentTarget == null)
        {
            enemyManager.currentTarget = enemyVitals.whoDealtDamageToMeLast;
        }

        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        //Debug.Log("Distance From Target (Combat Stance Strate): " + distanceFromTarget);


        if (enemyManager.isPerformingAction)
        {
            animationManager.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            HandleRotateTowardsTarget(enemyManager);

        }
        else
        {
            //if(!enemyManager.isInteracting)
            /* Reset our Colliders in case */
            animationManager.DeactivateMeleeColliderLeft();
            animationManager.DeactivateMeleeColliderRight();
        }

        if (enemyManager.currentRecoveryTime <= 0 && distanceFromTarget <= enemyManager.maximumAttackRange)
        {
            return attackState;
        }
        else if (distanceFromTarget <= enemyManager.maximumRangedAttackRange &&
            !enemyManager.spellOnCD && 
            enemyManager.isSpellCaster &&
            enemyManager.currentRecoveryTime <= 0) /* For Ranged attacks */
        {
            return attackState;
        }
        else if(distanceFromTarget > enemyManager.maximumAttackRange)
        {
            return pursueState;
        }
        else
        {
            return this; 
        }

        /* Check for attack range, circle around player */
        /* If in attack range go into attack state */
        /* If we are in a cool down after attacking return this state and keep circling player */
        /* If player runs out of range return the purse target state */

    }

 
    
    private void HandleRotateTowardsTarget(EC_EnemyManager enemyManager)
    {
        /* Rotate enemy manually to perform actions against player */
        if (enemyManager.isPerformingAction)
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            direction.y = 0;
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
        }


    }

}
