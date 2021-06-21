/*
 * This script controls selected random spawning for basic monsters.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class EnemySpawning : MonoBehaviour
   
{
    public GameObject enemy;
    public int enemyType;
    
    public GameObject[] spawns;
    //public int[] usedSpots;

    public int numEnemies;

    void Start()
    {
        SpawnEnemies(numEnemies);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnEnemies(int num)
    {
        int[] usedSpots = new int[num];

        for (int i = 0; i < num; i++)
        {
            bool done = false;
            while (!done)
            {
                int index = Random.Range(0, spawns.Length - 1);
                if (!usedSpots.Contains(index))
                {
                    usedSpots[i] = index;
                    Instantiate(enemy, spawns[index].transform.position, Quaternion.identity);
                    enemy.GetComponent<EC_EnemyManager>().mobID = -1; /* We do not want spawned mobs to change our light groups */
                    done = true;


                }
            }

        }
        LvlManager.Instance.FindMonsters();

    }

}
