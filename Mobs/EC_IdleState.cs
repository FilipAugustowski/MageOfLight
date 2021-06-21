using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_IdleState : EC_State
{
    public EC_PursueState pursueState;
    public EC_LobState lobState;

    void Start()
    {

    }

    public override EC_State Tick(EC_EnemyManager enemyManager, EC_EnemyVitals enemyVitals, EC_AnimatorController animationManager)
    {
        /* Look for potential target */
        /* Switch to pruse target state if target is found */
        /* If not target keep looking or patrol */
        #region Handle Enemy Target Detection
        
        Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, enemyManager.detectionRadius, enemyManager.detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            PC_EC_Vitals vitals = colliders[i].transform.GetComponent<PC_EC_Vitals>();

            if (vitals != null)
            {
                /* Check if friendly */

                Vector3 targetDirection = vitals.transform.position - enemyManager.transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

                if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                {
                    enemyManager.currentTarget = vitals;
                    float distance = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);


                    if (enemyManager.isStationary && distance <= 20f)
                    {
                        return lobState;
                    }

                }

            }
        }

        #endregion

        //#region Handle Switching To Next State
        /* this needs to be fixed */
        float distanceFromTarget = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, enemyManager.transform.position);
        if (enemyManager.currentTarget != null && !enemyManager.isStationary)
        {
            if (!enemyManager.keepIdle)
            {
                return pursueState;

            }
            else
            {
                return this;
            }


        }
        else
        {
            if (enemyManager.isStationary && distanceFromTarget <= 20f)
            {
                return lobState;
            }
            else
            {
                return this;

            }

        }
        //else
        //{
           
        //}
        //#endregion




    }
}
