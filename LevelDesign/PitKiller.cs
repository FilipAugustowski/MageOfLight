using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PitKiller : MonoBehaviour
{
    public bool isWater = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            other.gameObject.GetComponent<PC_EC_Vitals>().HandleDamage(1000000, 0, null);

            if (isWater) AudioManager.Instance.Play("WaterDeath");
            else AudioManager.Instance.Play("FallingDeath");

            //if(other.gameObject.GetComponent<PC_PlayerManager>())
            //{
            //    other.gameObject.GetComponent<PC_PlayerManager>().isDead = true;

            //}
            //else
            //{
            //    other.gameObject.GetComponent<EC_EnemyManager>().isDead = true;
            //}
        }

    }
}