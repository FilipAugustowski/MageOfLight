using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public PC_PlayerVitals vitals;
    public float strength = 50;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            vitals = other.gameObject.GetComponent<PC_PlayerVitals>();
            vitals.HealthRegen(strength);
            AudioManager.Instance.Play("PotionDrink");
            Destroy(gameObject);

        }
    }
}
