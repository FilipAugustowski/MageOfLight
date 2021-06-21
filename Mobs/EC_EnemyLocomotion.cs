using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EC_EnemyLocomotion : MonoBehaviour
{
    EC_EnemyManager enemyManager;


    private void Awake()
    {
        enemyManager = GetComponent<EC_EnemyManager>();
    }



}
