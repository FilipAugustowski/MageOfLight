using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_PlayerStats : MonoBehaviour
{
    /* Accesses */
    PC_MovementController movementController;

    public float movementSpeed = 10f;
    public float sprintMultiplier = 2.0f;
    public float maxMoveSpeed = 3f;
    public float maxSprintSpeed = 5f; 
    public float jumpForce = 7.0f;


    /* Damage, cast times, Health, and Mana will follow */

    void Awake()
    {
        SetupAllPlayerStats();
    }

    public void SetupAllPlayerStats()
    {
        SetupMovementController();
    }

    public void SetupMovementController()
    {
        movementController = GetComponent<PC_MovementController>();
        //movementController.SetupMovementStats(movementSpeed, sprintMultiplier, maxMoveSpeed, maxSprintSpeed, jumpForce);
    }
}
