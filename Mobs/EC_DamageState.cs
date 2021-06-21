using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_DamageState : EC_State
{
    public EC_State combatStanceState;
    public EC_State lobState;

    public override EC_State Tick(EC_EnemyManager enemyManager, EC_EnemyVitals enemyVitals, EC_AnimatorController animationManager)
    {
        animationManager.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        enemyManager.navMeshAgent.enabled = false;

        animationManager.DeactivateMeleeColliderLeft();
        animationManager.DeactivateMeleeColliderRight();

        if (enemyManager.isTakingDamage || enemyManager.isDead || enemyManager.isRooted)
        {
            return this;
        }
        else
        {
            if (!enemyManager.isStationary)
            {
                return combatStanceState;

            }
            else
            {
                return lobState;
            }
        }
    }
}
