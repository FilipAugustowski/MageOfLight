using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EC_HealthBar : MonoBehaviour
{
    EC_EnemyVitals vitals;
    public GameObject healthBar;
    public Slider barDisplay;
    public float distance;
    public Vector3 test;
    public GameObject playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        vitals = GetComponent<EC_EnemyVitals>();
        barDisplay = healthBar.GetComponentInChildren<Slider>();
    }
    private void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame

    public void UpdateBar()
    {
        barDisplay.value = vitals.health / vitals.maxHealth;
        float distanceFromTarget = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);
        Vector3 towards = (-playerCamera.transform.position + transform.position);
        test = towards;
        healthBar.transform.LookAt(playerCamera.transform.position, Vector3.up);

        if (distanceFromTarget <= distance && vitals.health > 0)
        {
            healthBar.SetActive(true);
        }
        else
        {
            healthBar.SetActive(false);
        }
    }
}
