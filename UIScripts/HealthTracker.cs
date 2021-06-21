using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* NO LONGER BEING USED */

public class HealthTracker : MonoBehaviour
{
    /* Access UI */
    int health = 100;

    public static HealthTracker Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        UIManager.Instance.SetupHealth(health);
    }

    public void HealthRegen(int _regenAmount)
    {
        if (health < 100)
        {
            health += _regenAmount;
        }
        else
        {
            health = 100;
        }

        SendHealthToUI();

    }

    public void DeductHealth(int _damage)
    {
        health -= _damage;
        SendHealthToUI();

        if (health <= 0)
        {
            SceneManager.LoadScene("deathscene");
        }
    }

    private void SendHealthToUI()
    {
        UIManager.Instance.UpdateHealth(health);
    }


    public void ResetHealth()
    {
        health = 100;
        SendHealthToUI();
    }
}
