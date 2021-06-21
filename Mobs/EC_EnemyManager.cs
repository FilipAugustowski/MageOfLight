using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EC_EnemyManager : MonoBehaviour
{
    public int monsterType;
    public bool isPerformingAction; /* Very similar to isInteracting in player manager, controlled by code */
    public bool isInteracting; /* Controlled by animator */
    public bool isTakingDamage;
    public bool isDead;
    public bool isRooted;

    public bool isBoss = false;

    public bool isSpellCaster = false;

    public float rootTime = 0;

    public bool gamePaused = false;
    public bool isStationary = false;
    //public EC_State idleState;

    public int mobID;

    EC_EnemyLocomotion enemyLocomotion;
    public EC_AnimatorController animatorController;
    EC_EnemyVitals enemyVitals;
    public PC_EC_Vitals currentTarget;
    public LayerMask detectionLayer;
    public EC_State currentState;
    public EC_State damageState;
    public NavMeshAgent navMeshAgent;
    public EC_HealthBar healthBar;
    public Rigidbody rigidbody;
    public bool keepIdle;


    public float maximumAttackRange = 1.5f;
    public float maximumRangedAttackRange = 8.0f;
    public float rotationSpeed = 5f;

    [Header("A.I Settings")]
    public float detectionRadius = 20;
    /* Field of view of Mob */
    public float maximumDetectionAngle = 50;
    public float minimumDetectionAngle = -50;

    /* Spells */
    [Header("Spell Settings")]
    public float spellCDTimer = 0.0f;
    public bool spellOnCD = false;

    public float currentRecoveryTime = 0;
    public float strafeTime = 0;
    public bool strafing = false;

    public NavMeshPath path;

    /* Ground Checks/Falling */
    [Header("Ground/Falling Settings")]
    public bool isGrounded;
    public bool isInAir;
    public float groundDetectionRayStartPoint = 0.2f;
    public float fallingSpeed = 500f;
    public float groundDetectionRayDistance = 0.2f;
    public float minimumDistanceNeededToBeginFall = 1f;
    public LayerMask ignoreForGroundChecks;
    public float inAirTimer = 0;
    //public EC_LobState lobState;

    [Header("Sounds")]
    public string[] attackSounds;

    private void Awake()
    {
        isPerformingAction = false;
        enemyLocomotion = GetComponent<EC_EnemyLocomotion>();
        animatorController = GetComponentInChildren<EC_AnimatorController>();
        enemyVitals = GetComponent<EC_EnemyVitals>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        path = new NavMeshPath();
        healthBar = GetComponent<EC_HealthBar>();


    }
    private void Start()
    {
        keepIdle = false;
        navMeshAgent.enabled = false;
        rigidbody.isKinematic = false;
        ///idleState = GetComponentInChildren<>

    }

    private void FixedUpdate()
    {
        GroundChecks();
    }

    private void Update()
    {
        isInteracting = animatorController.animator.GetBool("IsInteracting");
        isTakingDamage = animatorController.animator.GetBool("IsTakingDamage");
        isDead = animatorController.animator.GetBool("IsDead");
        HandleRecoveryTime();
        HandleStateMachine(); 
        HandleStrafingTime();
        HandleSpellCD();
        HandleRootTime();
        enemyVitals.HandleKnockBackTimer();
        if(!enemyVitals.isDummy)healthBar.UpdateBar();

        if (keepIdle)
        {
            if(currentState != GetComponentInChildren<EC_IdleState>())
            {
                SwitchToNextState(GetComponentInChildren<EC_IdleState>());;
            }
        }
    }


    private void LateUpdate()
    {
        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;
    }


    private void HandleStateMachine()
    {
        if (currentState != null)
        {
            EC_State nextState = currentState.Tick(this, enemyVitals, animatorController);



            if (nextState != null)
            {
                if(isDead || isTakingDamage)
                {
                    SwitchToNextState(damageState);
                }
                else
                {
                    SwitchToNextState(nextState);
                }
            }
        }
    }

    public void SwitchToNextState(EC_State _state)
    {
        currentState = _state;
    }

    private void GroundChecks()
    {
        isGrounded = false;
        RaycastHit hit;
        Vector3 origin = transform.position;
        origin.y += groundDetectionRayStartPoint;

        //if (Physics.Raycast(origin, transform.forward, out hit, 0.4f))
        //{
        //    moveDirection = Vector3.zero;
        //} Add to pursueState HandleFalling, maybe not might break AI


        if (isInAir) //Add to pursue State maybe
        {
            rigidbody.AddForce(-Vector3.up * fallingSpeed); /* May take out delta time */
         
        }


        Vector3 dir = rigidbody.velocity;
        dir.Normalize();
        origin = origin + dir * groundDetectionRayDistance;

        Vector3 targetPosition = transform.position;
        UnityEngine.Debug.DrawRay(origin, -Vector3.up * minimumDistanceNeededToBeginFall, Color.red, 0.1f, false);
        
        if (Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, ignoreForGroundChecks)) /* Could add ignore for ground checks here */
        {
            if (hit.transform.gameObject.layer == 12)
            {
                isDead = true;
            }


            Vector3 normalVector = hit.normal;
            Vector3 tp = hit.point;
            isGrounded = true;
            targetPosition.y = tp.y;

            if (isInAir)
            {
                if (inAirTimer > 0.5f)
                {
                    //animatorController.PlayTargetAnimation("PC_Landing", true, 1);
                    inAirTimer = 0;
                }
                else
                {
                    inAirTimer = 0;
                }

                isInAir = false;

            }
        }
        else
        {
            if (isGrounded)
            {
                isGrounded = false;
            }

            if (!isInAir)
            {
                //if (!isInteracting)
                //{
                //    animatorController.PlayTargetAnimation("PC_Falling", true, 1);
                //}

                Vector3 vel = rigidbody.velocity;
                vel.Normalize();
                rigidbody.velocity = vel * 6.0f;

                isInAir = true;
            }

            //if (isGrounded)
            //{
            //    if (isInteracting || inputManager.moveAmount > 0)
            //    {
            //        myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime);
            //    }
            //    else
            //    {
            //        myTransform.position = targetPosition;
            //    }
            //}
        }

    }



    private void HandleRecoveryTime()
    {
        if(currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if(isPerformingAction)
        {
            if(currentRecoveryTime <= 0)
            {
                isPerformingAction = false;
            }
        }
    }

    private void HandleStrafingTime()
    {
        if (strafeTime > 0)
        {
            strafeTime -= Time.deltaTime;
        }

        if (strafing)
        {
            if (strafeTime <= 0)
            {
                strafing = false;
            }
        }
    }

    private void HandleRootTime()
    {
        if (rootTime > 0)
        {
            rootTime -= Time.deltaTime;
        }

        if (isRooted)
        {
            if (rootTime <= 0)
            {
                isRooted = false;
            }
        }
    }

    private void HandleSpellCD()
    {
        if (spellCDTimer > 0)
        {
            spellCDTimer -= Time.deltaTime;
        }

        if (spellOnCD)
        {
            if (spellCDTimer <= 0)
            {
                spellOnCD = false;
            }
        }
    }
    public void ProperReset()
    {
        GetComponent<EC_EnemyVitals>().health = GetComponent<EC_EnemyVitals>().maxHealth;
        animatorController.StopCurrentAnimation();

        /* I think this fixes mobs running at player after respawn, requires testing of each checkpoint in game */
        SwitchToNextState(GetComponentInChildren<EC_IdleState>());
        keepIdle = true;
        Invoke("returnToNormal", 5f);

    }
    public void returnToNormal()
    {
        keepIdle = false;
    }

}
