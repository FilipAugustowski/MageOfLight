using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_AttackState : EC_State
{
    public EC_CombatStanceState combatStanceState;
    public EC_Strafe strafeState;

    public EC_EnemyAttackAction[] enemyAttacks;
    public EC_EnemyAttackAction currentAttack;

    int iterationsBeforeAttackIsChosen = 0;

    public override EC_State Tick(EC_EnemyManager enemyManager, EC_EnemyVitals enemyVitals, EC_AnimatorController animationManager)
    {
        //Debug.Log("Iterations before attack is chosen: " + iterationsBeforeAttackIsChosen);
        /* Select one of array of attacks based on attack scores */
        /* If the selected attack is not able to be used because of bad angle or distance, select a new attack
         * if the attack is viable stop our movement and attack our target 
         * Set our recovery timer to the attacks recovery time 
         * return to the combat stand */
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

        //Debug.Log("Distance From Target (AttackState): " + distanceFromTarget);

        if (enemyManager.isPerformingAction) return combatStanceState;

        HandleRotateTowardsTarget(enemyManager);


        if (currentAttack != null) /* If too close, get different attack */
        {
            //iterationsBeforeAttackIsChosen = 0;
            if (distanceFromTarget < currentAttack.minimumDistanceNeededToAttack)
            {
                return this;
            }
            else if(distanceFromTarget < currentAttack.maximumDistanceNeededToAttack)
            {
                if(viewableAngle <= currentAttack.maximumAttackAngle &&
                    viewableAngle >= currentAttack.minimumAttackAngle)
                {
                    if (enemyManager.currentRecoveryTime <= 0 && !enemyManager.isPerformingAction)
                    {
                        animationManager.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                        animationManager.animator.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);

                        /* check that the current attack action has a spellobject, if so then set it in the animator as the current spell object */ 
                        if (currentAttack.hasSpell)
                        {
                            enemyManager.spellOnCD = true;
                            enemyManager.spellCDTimer = currentAttack.spellCD;
                            animationManager.currSpell = currentAttack.spell;
                        }


                        animationManager.PlayTargetAnimation(currentAttack.actionAnimation, true, 1);
                        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
                        enemyManager.isPerformingAction = true;

                        animationManager.currDamage = currentAttack.damage;
                        animationManager.currForce = currentAttack.force;



                        if (enemyManager.attackSounds.Length > 0)
                        {
                            int x = enemyManager.attackSounds.Length;

                            int random = Random.Range(0, x);

                            AudioManager.Instance.Play(enemyManager.attackSounds[random]);

                        }
                        else
                        {
                            AudioManager.Instance.Play("Monster1Swipe");
                        }

                        currentAttack = null;



                        return combatStanceState; /* WAS COMBAT STANCE STATE MUST CHANGE BACK */
                    }
                }
            }
        }
        else
        {
            Debug.Log("Attack not chosen");
            GetNewAttack(enemyManager);
            //iterationsBeforeAttackIsChosen++;
        }

        return combatStanceState;
    }


    private void GetNewAttack(EC_EnemyManager enemyManager)
    {
        Vector3 targetsDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float viewableAngle = Vector3.Angle(targetsDirection, enemyManager.transform.forward);
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        int maxScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EC_EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                    && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    maxScore += enemyAttackAction.attackScore;
                }
            }
        }

        int randomValue = Random.Range(0, maxScore);
        int tempScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EC_EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                    && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    if (currentAttack != null) return;

                    tempScore += enemyAttackAction.attackScore;

                    if (tempScore > randomValue)
                    {
                        if (enemyAttackAction.hasSpell)
                        {
                            if(!enemyManager.spellOnCD) /* If the mob has recently used a spell then dont choose it */
                            {
                                currentAttack = enemyAttackAction;

                            }
                        }
                        else
                        {
                            currentAttack = enemyAttackAction;
                        }
                    }
                }
            }
        }
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
