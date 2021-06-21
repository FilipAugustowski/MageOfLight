using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_Strafe : EC_State
{
    public EC_PursueState pursueStance;
    private Vector3 currentStrafePoint;

    public override EC_State Tick(EC_EnemyManager enemyManager, EC_EnemyVitals enemyVitals, EC_AnimatorController animationManager)
    {
        HandleStrafe(enemyManager);

        if(!enemyManager.strafing)
        {
            return pursueStance;

        }
        else
        {
            return this;
        }

    }
    private void HandleStrafe(EC_EnemyManager enemyManager)
    {
        /* If the player was not strafing, give them a destination and strafe */
        var randomStrafePoint = enemyManager.transform.position + Random.insideUnitSphere * 3;
        currentStrafePoint = randomStrafePoint;


    }

}

//if (!enemyManager.strafing)
//{
//    /* If the player was not strafing, give them a destination and strafe */
//    enemyManager.strafing = true;
//    var randomStrafePoint = enemyManager.transform.position + Random.insideUnitSphere * 3;
//    currentStrafePoint = randomStrafePoint;
//}

//Vector3 targetVelocity = enemyManager.rigidbody.velocity;
//enemyManager.navMeshAgent.enabled = true;

//enemyManager.navMeshAgent.SetDestination(currentStrafePoint);
////enemyManager.rigidbody.velocity = targetVelocity;
////enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);


//if(Vector3.Distance(enemyManager.transform.position, currentStrafePoint) < 2.0f)
//{
//    enemyManager.strafing = false;
//    return true;
//}
//else
//{
//    return false;
//}


//enemyManager.navMeshAgent.enabled = true;
//var offsetPlayer = (enemyManager.transform.position - enemyManager.currentTarget.transform.position);
////Vector3 targetPosition = offsetPlayer * Random.Range(1.0f, 4.0f);

//var dir = Vector3.Cross(offsetPlayer, Vector3.up);
//Vector3 destination = (enemyManager.transform.position + dir); /* add strafe weight var */

////NavMesh.CalculatePath(enemyManager.transform.position, destination, enemyManager.navMeshAgent.areaMask, enemyManager.path);
//enemyManager.navMeshAgent.SetDestination(destination);

//var lookPos = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
//lookPos.y = 0;
//var rotation = Quaternion.LookRotation(lookPos);
//enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, rotation, Time.deltaTime * 25);
