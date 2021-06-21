using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EC_State : MonoBehaviour
{
    public abstract EC_State Tick(EC_EnemyManager enemyManager, EC_EnemyVitals enemyVitals, EC_AnimatorController animationManager);

}
