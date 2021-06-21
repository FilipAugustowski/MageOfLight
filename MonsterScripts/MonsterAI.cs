using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* ANIMATIONS: normal movement like walking, patrolling, strafing, dying will be directly shut off and on from this script. Any sort of attacking will be turned on here, but turned off 
 * in the animator because we do not want to interupt our attack animations UNLESS the monster is hit. There is a fundamental difference between locomotion and attacking which requires this 
 * split. 
*/

/* Sources: Starting point: https://www.youtube.com/watch?v=UjkSFoLxesw&ab_channel=Dave%2FGameDevelopment */

public class MonsterAI : MonoBehaviour
{
    //protected LvlManager levelManager; /* Need to tell level manager stuff from time to time, AI death, AI encounter player, etc. */

    //public int monsterID = -1; /* If this monster does not have narratives or does not activate a light group than make this ID -1 */

    ///* Narrative Stuff */
    //protected int narrativeID;
    //protected bool hasEncounteredPlayer = false;

    //public NavMeshAgent agent;

    //protected Transform player;

    //public LayerMask whatIsGround, whatIsPlayer;

    //public GameObject deathOrbPrefab;

    //protected bool cancelForce = false;

    //protected bool patrolling = false;

    //protected bool movementDisabled = false;

    ///* BASE MONSTERS DO NOT HAVE MULTIPLE ATTACKS YET */
    //public GameObject model;


    ///* States */
    //public enum State
    //{
    //    PATROL,
    //    CHASE,
    //    DIE,
    //    ATTACK,
    //    DISENGAGE,
    //    TAKINGDAMAGE,
    //    STRAFE,
    //    APPROACHINGFORATTACK,
    //    IDLING,
    //    ROOTED
    //}

    ///* State Logic */
    //bool instantiated = false; /* Used to start the FSM for the monster */
    //public State state;
    ////State currState;
    ////State lastState;

    //protected bool rooted = false;

    ///* Physics */
    //Rigidbody rigidbody;
    //float initialDragForce = 1.0f;
    //float initialSpeed = 2.5f;
    //public float currForceApplied = 0.0f;
    //Vector3 currForceVelocity;
    //bool forceApplied = false;
    //float tempHitforce;

    ///* Stats */
    //public float health;

    ///* Patrolling Logic */
    ////[HideInInspector]
    //public Vector3 walkPoint;
    //public bool walkPointSet;
    //public float walkPointRange;
    //protected float patrolSpeed = 1.0f;

    ///* Attacking */
    //public float timeBetweenAttacks;
    //protected bool alreadyAttacked;
    ////MeleeCollider attack1Collider;
    //int attack1Damage = 10;

    ///* Disengage */
    //public float disengageForce = 100.0f;
    //bool resettingDisengage = false;
    //int disengageRandomVar = 0;
    //bool canRandomDisengage = true;
    //protected bool forceLateralDisengage = false;
    //bool forceBackDisengage = false;
    //protected Vector3 disengageDirection;

    ///* Strafe */
    //int strafeRandomVar = 0;
    //bool resettingStrafe = false;
    //float enemyRotationSpeed = 10.0f;
    //public float strafeSpeed = 1.0f;
    //protected bool forceLateralStrafe = false;
    //protected bool forceBackStrafe = false;
    //protected bool canRandomStrafe = true;
    //protected float randomStrafeTimer = 6.0f;
    //protected float strafeTimeMin = 1.5f;
    //protected float strafeTimeMax = 2.5f;
    //protected float strafeWeight = 10.0f;

    ///* Approach */
    //public float approachSpeed = 1.8f;

    ///* Chase */
    //public float chaseSpeed = 3.3f;

    ///* Movement Choice Variables */
    //public float sightRange, attackRange, approachRange;
    //[HideInInspector]
    //public bool playerInSightRange;

    ///* Idle */
    //protected float idleTimer = 0.0f;
    //protected float idleTime = 0.2f;


    ///* Death */
    //protected float deathTimer = 0.6f;
    //bool enteringTakeDamage = false;

    //public bool alive = true;



    ///* Animation stuff, should make seperate class later */
    //[HideInInspector]
    //public Animator animManager;
    //[HideInInspector]
    //public MonsterAnimatorController monsterAnimInfo;

    ///* Extra */
    //RFX4_EffectSettings[] handEffects;
    //public bool hasHandEffects = true;


    ///* workin on it */
    //protected NavMeshPath path;

    ///* If navmesh stops working */
    //bool agentHasPathAndAgentIsRunning;
    //Vector3 lastPosition;
    //float giveUpTime = 0.5f;


    ///* Helps visualize Ranges */
    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, sightRange);
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}

    //protected virtual void Awake()
    //{
    //    agent = GetComponent<NavMeshAgent>();
    //    rigidbody = GetComponent<Rigidbody>();
    //    agent.speed = initialSpeed;
    //    animManager = GetComponent<Animator>();
    //    monsterAnimInfo = GetComponent<MonsterAnimatorController>();

    //    attack1Collider = GetComponentInChildren<MeleeCollider>();
    //    //attack1Collider.Setup(whatIsPlayer, attack1Damage, 5);

    //    if(hasHandEffects)
    //    {
    //        handEffects = GetComponentsInChildren<RFX4_EffectSettings>();
    //        DisableHandEffects();
    //    }

    //    walkPointSet = false;

    //    path = new NavMeshPath();

    //}

    //protected virtual void Start()
    //{
    //    player = PlayerTracker.Instance.transform;
    //    levelManager = LvlManager.Instance;
    //    agentHasPathAndAgentIsRunning = true;

    //}

    //void LateUpdate()
    //{
    //    //START Finite State Machine once
    //    if (!instantiated)
    //    {
    //        state = State.PATROL;
    //        StartCoroutine("FSM");
    //        instantiated = true;
    //    }
    //    if (agent.isActiveAndEnabled && agent.hasPath)
    //    {
    //        Vector3 effectiveVelocity = (transform.position - lastPosition) / Time.deltaTime;

    //        effectiveVelocity.y = 0f;

    //        if (effectiveVelocity.sqrMagnitude > giveUpTime) // Tune this threshold to your needs.
    //            transform.rotation = Quaternion.LookRotation(effectiveVelocity);
    //    }

    //    lastPosition = transform.position;
    //}



    ////FINITE STATE MACHINE
    ////This enumerator updates to see what action the AI Should perform given the state of the AI which is passed into the enumerator 
    //IEnumerator FSM()
    //{
    //    while (alive)
    //    {
    //        //ForceUpdate();
    //        switch (state)
    //        {
    //            case State.PATROL:
    //                Patroling();
    //                break;
    //            case State.CHASE:
    //                ChasingPlayer();
    //                break;
    //            case State.ATTACK:
    //                AttackingPlayer();
    //                break;
    //            case State.APPROACHINGFORATTACK:
    //                ApproachingForAttack();
    //                break;
    //            case State.DISENGAGE:
    //                Disengage();
    //                break;
    //            case State.STRAFE:
    //                Strafe();
    //                break;
    //            case State.TAKINGDAMAGE:
    //                TakingDamage();
    //                break;
    //            case State.IDLING:
    //                Idling();
    //                break;
    //            case State.ROOTED:
    //                Rooted();
    //                break;
    //        }
    //        yield return null;
    //    }
    //}

    ///* Inspired by https://gist.github.com/ditzel/1f207c838f0023fcbd34c5c67955fd25 */
    //protected IEnumerator ApplyForceAsVelocity(Vector3 _direction, float _desiredForceTime, float _hitForce = 1, float _forceTimer = 0, ForceMode _mode = ForceMode.Impulse)
    //{
    //    if(forceApplied) /* just add the force if a current force is currently working */
    //    {
    //        rigidbody.AddForce(_direction * _hitForce, _mode);

    //        yield break;
    //    }
    //    forceApplied = true;
    //    DisableMonsterMovement();

    //    //Time.timeScale = 0.0f;

    //    rigidbody.AddForce(_direction * _hitForce, _mode);

    //    while (_forceTimer < _desiredForceTime && !cancelForce)
    //    {
    //        _forceTimer += Time.fixedDeltaTime;

    //        agent.velocity = rigidbody.velocity;

    //        transform.LookAt(player);

    //        yield return null;
    //    }
    //    EnableMonsterMovement();

    //}

    //protected virtual void Idling()
    //{
    //    /* purge all animations */
    //    animManager.SetBool("Idling", true);
    //    transform.LookAt(player);
    //    agent.destination = transform.position;

    //    idleTimer += Time.deltaTime;

    //    if (CheckIfPlayerInAttackRange() && !alreadyAttacked)
    //    {
    //        animManager.SetBool("Idling", false);
    //        state = State.ATTACK;
    //    }

    //    if(CheckIfPlayerInApproachRange() && !CheckIfPlayerInAttackRange())
    //    {
    //        animManager.SetBool("Idling", false);
    //        state = State.APPROACHINGFORATTACK;
    //    }

    //    else if(!CheckIfPlayerInApproachRange())
    //    {
    //        animManager.SetBool("Idling", false);
    //        state = State.CHASE;
    //    }

    //    else if(idleTimer >= idleTime)
    //    {
    //        idleTimer = 0;
    //        animManager.SetBool("Idling", false);
    //        forceBackStrafe = true;
    //        state = State.STRAFE;
    //    }
    //}

    ///* States */
    //protected virtual void Patroling() /* Update to state machine, is only called in the awake function */
    //{
    //    patrolling = true;
    //    /* Anim Updates */
    //    animManager.SetBool("Patroling", true);

    //    agent.isStopped = false;
    //    agent.speed = patrolSpeed;

    //    if (!walkPointSet) SearchWalkPoint();

    //    if (walkPointSet)
    //        if (!NavMesh.CalculatePath(transform.position, walkPoint, agent.areaMask, path)) walkPointSet = false;
    //        else agent.SetPath(path);

    //    Vector3 distanceToWalkPoint = transform.position - walkPoint;

    //    /* Reached the destination */
    //    if (distanceToWalkPoint.magnitude <= agent.stoppingDistance)
    //        walkPointSet = false;

    //    if (CheckIfPlayerInChaseRange())
    //    {
    //        AudioManager.Instance.Play("Monster1NoticePlayer");
    //        animManager.SetBool("Patroling", false);
    //        patrolling = false;
    //        LvlManager.Instance.NotifyInCombat();
    //        state = State.CHASE;
    //    }

    //}

    //virtual protected void ChasingPlayer()
    //{
    //    animManager.SetBool("Chasing", true); /* dont really need this to be called every frame */
    //    agent.speed = chaseSpeed;

    //    if(!hasEncounteredPlayer)
    //    {
    //        hasEncounteredPlayer = true;
    //        //levelManager.SignalNarrativeMonsterEncounter(narrativeID);
    //    }

    //    if (CheckIfPlayerInApproachRange())
    //    {
    //        animManager.SetBool("Chasing", false);
    //        state = State.APPROACHINGFORATTACK;
    //    }
    //    if (CheckIfPlayerInAttackRange())
    //    {
    //        animManager.SetBool("Chasing", false);
    //        state = State.ATTACK;
    //    }

    //    if(RandomStrafe())
    //    {
    //        animManager.SetBool("Approaching", false);
    //        forceLateralStrafe = true;
    //        state = State.STRAFE;
    //    }

    //    agent.isStopped = false;

    //    NavMesh.CalculatePath(transform.position, PlayerTracker.Instance.transform.position, agent.areaMask, path);
    //    agent.SetPath(path);
    //    //agent.SetDestination();
    //}

    //protected virtual void ApproachingForAttack()
    //{
    //    animManager.SetBool("Approaching", true);

    //    transform.LookAt(player);
    //    agent.speed = approachSpeed;

    //    NavMesh.CalculatePath(transform.position, PlayerTracker.Instance.transform.position, agent.areaMask, path);
    //    agent.SetPath(path);


    //    if (RandomDisengage())
    //    {
    //        animManager.SetBool("Approaching", false);
    //        forceLateralDisengage = true;
    //        state = State.DISENGAGE;

    //    }
    //    if (CheckIfPlayerInAttackRange() && !alreadyAttacked)
    //    {
    //        animManager.SetBool("Approaching", false);
    //        state = State.ATTACK;
    //    }
    //    if(CheckIfPlayerInAttackRange() && alreadyAttacked)
    //    {
    //        animManager.SetBool("Approaching", false);
    //        state = State.IDLING;
    //    }
    //    if (RandomStrafe())
    //    {
    //        animManager.SetBool("Approaching", false);
    //        forceLateralStrafe = true;
    //        state = State.STRAFE;

    //    }

    //}


    //protected virtual void AttackingPlayer()
    //{
    //   /* Anim Updates */
    //    animManager.SetBool("Chasing", false);
    //    animManager.SetBool("Patroling", false);
    //    animManager.SetBool("Approaching", false);

    //    transform.LookAt(player);

    //    /* Collision Detection for melee attack is done on the animation itself */

    //    if(!alreadyAttacked)
    //    {
    //        EnableHandEffects();
    //        animManager.SetBool("Attacking", true); /* Choose anim attack */
    //        AudioManager.Instance.Play("Monster1Swipe");
    //        alreadyAttacked = true;


    //        Invoke(nameof(ResetAttack), timeBetweenAttacks); 

    //    }
    //    else
    //    {
    //        if(!animManager.GetBool("Attacking"))
    //        {
    //            DisableHandEffects();
    //            state = State.APPROACHINGFORATTACK;
    //        }

    //    }
    //}

    //public virtual void ApplyAttackForce()
    //{
    //    if(animManager.GetBool("Attacking"))
    //    {
    //        transform.LookAt(player);

    //        /* Want them to go towards the player */
    //        Vector3 direction = (player.transform.position - transform.position).normalized;
    //        float attack1Force = 7.0f;

    //        StartCoroutine(ApplyForceAsVelocity(direction, monsterAnimInfo.GetDisengageAnimationTime(), attack1Force));
    //    }
    //}

    //public virtual void ApplyDisengageForce()
    //{
    //    transform.LookAt(player);

    //    StartCoroutine(ApplyForceAsVelocity(disengageDirection, monsterAnimInfo.GetDisengageAnimationTime(), disengageForce));
    //}



    //protected void Disengage() /* State machine updated, the statement should only be called once and the reset function bring us back to chasing */
    //{
    //    if(!animManager.GetBool("Disengaging") && !resettingDisengage)
    //    {
    //        agent.isStopped = true;

    //        disengageDirection = ChooseDisengageDirection();
            
    //        animManager.SetInteger("DisengageDirection", disengageRandomVar);
    //        animManager.SetBool("Disengaging", true);
    //        resettingDisengage = true;

    //        //AudioManager.Instance.Play("MonsterGrowl");

    //        /* DISENGAGE IS RESET FROM THE ANIMATION MONSTERAJUMP */
    //    }
        
    //}


    //protected void Strafe()
    //{
    //    /* This is called first and then based on the direction the other three ifs will fire as updates */
    //    if(!animManager.GetBool("Strafing") && !resettingStrafe)
    //    {
    //        Vector3 strafeDirection = ChooseStrafeDirection();


    //        animManager.SetInteger("StrafeDirection", strafeRandomVar);
    //        animManager.SetBool("Strafing", true);
    //        resettingStrafe = true;

    //        agent.speed = strafeSpeed;

    //        /* Pick a random strafe time */
    //        float strafeTime = Random.Range(strafeTimeMin, strafeTimeMax);

    //        Invoke(nameof(ResetStrafe), strafeTime);
    //    }
    //    /* Back strafe https://answers.unity.com/questions/1319111/navmesh-agent-move-back-when-player-comes-to-close.html */
    //    else if (animManager.GetBool("Strafing") && resettingStrafe && strafeRandomVar == 0)
    //    {
    //        var offsetPlayer = (transform.position - player.position).normalized;
    //        //Vector3 targetPosition = offsetPlayer * Random.Range(1.0f, 4.0f);

    //        //var dir = Vector3.Cross(offsetPlayer, Vector3.right);

    //        //NavMesh.CalculatePath(transform.position, (transform.position + dir) * Random.Range(1.0f, 4.0f), agent.areaMask, path);
    //        // agent.SetPath(path);

    //        Vector3 destination = (transform.position + offsetPlayer * strafeWeight);


    //        //agent.SetDestination(destination);

    //        //agent.updateRotation = false;

    //        ModelFacePlayer();

    //        NavMesh.CalculatePath(transform.position, destination, agent.areaMask, path);
    //        agent.SetPath(path);

    //        //transform.LookAt(player);

    //        //var lookPos = player.position - transform.position;
    //        //lookPos.y = 0;
    //        //var rotation = Quaternion.LookRotation(lookPos);
    //        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * enemyRotationSpeed);
    //    }
    //    else if(animManager.GetBool("Strafing") && resettingStrafe && strafeRandomVar == 1) /* Strafe around the player left */
    //    {
    //        var offsetPlayer = transform.position - player.position;
    //        var dir = Vector3.Cross(offsetPlayer, Vector3.up);
    //        NavMesh.CalculatePath(transform.position, transform.position + dir, agent.areaMask, path);
    //        agent.SetPath(path);
    //        var lookPos = player.position - transform.position;
    //        lookPos.y = 0;
    //        var rotation = Quaternion.LookRotation(lookPos);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * enemyRotationSpeed);
    //    }
    //    else if (animManager.GetBool("Strafing") && resettingStrafe && strafeRandomVar == 2) /* Strafe around the player left */
    //    {
    //        var offsetPlayer = player.position - transform.position;
    //        var dir = Vector3.Cross(offsetPlayer, Vector3.up);
    //        NavMesh.CalculatePath(transform.position, transform.position + dir, agent.areaMask, path);
    //        agent.SetPath(path);
    //        var lookPos = player.position - transform.position;
    //        lookPos.y = 0;
    //        var rotation = Quaternion.LookRotation(lookPos);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * enemyRotationSpeed);
    //    }

    //    //if (agent.remainingDistance <= agent.stoppingDistance + 1)
    //    //{
    //    //    Debug.Log("Called remaiing distance");
    //    //    strafeWeight += 4;

    //    //    //agent.ResetPath();
    //    //}

    //}

    //protected virtual void MakeCalculatedDecision() /* needs a lot of more */
    //{
    //    if (player.GetComponent<AnimatorController>().CheckIfPlayerPreppingAttack())
    //    {
    //        state = State.DISENGAGE;
    //    }

    //    else
    //    {
    //        int y = Random.Range(0, 10); /* Choose between disengage or strafe SHOULD CHANGE TO DISENGAGE OR ATTACK AFTER ATTACK, THEN DISENGAGE LEADS INTO STRAFE OR GO APPROACHINGFORATTACK*/

    //        if (y <= 7)
    //        {
    //            forceBackStrafe = true;
    //            state = State.STRAFE;
    //        }
    //        else
    //        {
    //            state = State.APPROACHINGFORATTACK;
    //        }
    //    }
    //}


    //public virtual void TakeDamage(int _damage, float _hitForce, bool _heavyHit = false)
    //{
    //    Debug.Log("ow");
    //    if (alive)
    //    {
    //        /* Anim Updates */
    //        animManager.SetBool("Chasing", false);
    //        animManager.SetBool("Strafing", false);
    //        animManager.SetBool("Patroling", false);
    //        animManager.SetBool("Approaching", false);
    //        animManager.SetBool("Disengaging", false);
    //        animManager.SetBool("Attacking", false);
    //        animManager.SetBool("Idling", false);
    //        health -= _damage;

    //        if (!animManager.GetBool("TakingDamage"))
    //        {
    //            if (!rooted)
    //            {
    //                animManager.SetInteger("TakingDamageID", 0);

    //            }
    //            else
    //            {
    //                animManager.SetInteger("TakingDamageID", 1);
    //                animManager.SetBool("Rooted", false);
    //                health -= 20; /* take extra damage if rooted */
    //            }

    //            animManager.SetBool("TakingDamage", true);

    //            enteringTakeDamage = true;
    //            tempHitforce = _hitForce;
    //            state = State.TAKINGDAMAGE; /* State switch here */

    //            /* Taking damage animation is reset in animation */
    //        }
    //    }
    //}

    //protected virtual void TakingDamage()
    //{
    //    if(enteringTakeDamage && !rooted)
    //    {
    //        enteringTakeDamage = false;
    //        Vector3 direction = Vector3.Normalize(transform.position - player.position);

    //        StartCoroutine(ApplyForceAsVelocity(direction, monsterAnimInfo.GetTakingDamageAnimationTime(), tempHitforce));
    //    }
    //    if (health <= 0 && alive)
    //    {
    //        alive = false;
    //        animManager.SetBool("TakingDamage", false);
    //        animManager.SetBool("Dying", true);
    //        Invoke(nameof(Die), deathTimer);
    //    }
    //    if (!animManager.GetBool("TakingDamage"))
    //    {
    //        agent.velocity = new Vector3(0, 0, 0);
    //        state = State.CHASE;

    //    }

    //}

    //protected virtual void Rooted()
    //{
    //    if(!rooted)
    //    {
    //        state = State.APPROACHINGFORATTACK;
    //    }
    //}


    //protected virtual void Die()
    //{
    //    if(monsterID >= 0)
    //    {
    //        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    //        GameObject absorbtionOrb = Instantiate(deathOrbPrefab, spawnPosition, Quaternion.identity);
    //        absorbtionOrb.GetComponent<MonsterDeathOrb>().PassInLightingLogic(this);
    //    }

    //    GetComponent<Collider>().enabled = false;
    //    GetComponentInChildren<Light>().enabled = false;
    //}

    //protected virtual bool CheckIfPlayerInAttackRange()
    //{
    //    return Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
    //}

    //protected bool CheckIfPlayerInApproachRange()
    //{
    //    return Physics.CheckSphere(transform.position, approachRange, whatIsPlayer);
    //}

    //protected bool CheckIfPlayerInChaseRange()
    //{
    //    return Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
    //}

    //protected bool CheckIfPlayerInRange(float _radiusRange)
    //{
    //    return Physics.CheckSphere(transform.position, _radiusRange, whatIsPlayer);
    //}

    //protected void SearchWalkPoint()
    //{
    //    float randomZ = Random.Range(-walkPointRange, walkPointRange);
    //    float randomX = Random.Range(-walkPointRange, walkPointRange);

    //    walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    //    if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
    //    {
    //        walkPointSet = true;
    //    }
    //}

    //protected virtual bool RandomDisengage(int _mustBeGreaterThan = 80)
    //{
    //    if (canRandomDisengage & !resettingDisengage)
    //    {
    //        if(player.GetComponent<AnimatorController>().CheckIfPlayerPreppingAttack())
    //        {
    //            if (player.GetComponent<AnimatorController>().CheckIfSpellCasting())
    //            {
    //                forceLateralDisengage = true;
    //            }
    //            else
    //            {
    //                forceBackDisengage = true;
    //            }

    //            int x = Random.Range(0, 100);

    //            if (x >= _mustBeGreaterThan)
    //            {
    //                canRandomDisengage = false;
    //                Invoke(nameof(ResetRandomDisengage), 7.0f);
    //                return true;
    //            }
    //            else
    //            {
    //                return false;
    //            }
    //        }
    //        else
    //        {
    //            return false;
    //        }

    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //protected virtual bool RandomStrafe()
    //{
    //    if (canRandomStrafe)
    //    {
    //        canRandomStrafe = false;
    //        Invoke(nameof(ResetRandomStrafe), randomStrafeTimer); /* Checks every 2 seconds if for a 2/10 chance to strafe either left or right */
    //        int x = Random.Range(0, 100);

    //        if (x >= 70)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //Vector3 ChooseDisengageDirection()
    //{
    //    if (forceLateralDisengage)
    //    {
    //        disengageRandomVar = Random.Range(1, 3);
    //    }
    //    else if (forceBackDisengage) /* if forcing, return back direction */
    //    {
    //        Vector3 disengageDirection = Vector3.Normalize(transform.position - player.position);
    //        disengageRandomVar = 0;
    //        return disengageDirection; /* Back */
    //    }
    //    else
    //    {
    //        disengageRandomVar = Random.Range(0, 3);
    //    }

    //    if(disengageRandomVar == 0)
    //    {
    //        Vector3 disengageDirection = Vector3.Normalize(transform.position - player.position);
    //        return disengageDirection; /* Back */
    //    }
    //    else if (disengageRandomVar == 1)
    //    {
    //        Vector3 disengageDirection = Vector3.Normalize(-transform.forward + transform.right);
    //        return disengageDirection; /* Left */
    //    }
    //    else if (disengageRandomVar == 2)
    //    {
    //        Vector3 disengageDirection = Vector3.Normalize(-transform.forward - transform.right);

    //        return disengageDirection; /* Right */
    //    }

    //    return -transform.forward; /* Back, but should never hit this case */
    //}

    //Vector3 ChooseStrafeDirection()
    //{
    //    if (forceLateralStrafe)
    //    {
    //        strafeRandomVar = Random.Range(1, 3);
    //    }
    //    else if (forceBackStrafe)
    //    {
    //        strafeRandomVar = 0;
    //    }
    //    else
    //    {
    //        strafeRandomVar = Random.Range(0, 3);

    //    }

    //    if (disengageRandomVar == 0)
    //    {
    //        return -transform.forward; /* Back */
    //    }
    //    else if (disengageRandomVar == 1)
    //    {
    //        return -transform.right; /* Left */
    //    }
    //    else if (disengageRandomVar == 2)
    //    {
    //        return transform.right; /* Right */
    //    }

    //    return -transform.forward; /* Back, but should never hit this case */
    //}

    //protected Vector3 GetDirectionTowardsPlayer()
    //{
    //    return (player.transform.position - transform.position).normalized;
    //}

    //protected Vector3 GetDirectionAwayFromPlayer()
    //{
    //    return (transform.position - player.transform.position).normalized;
    //}

    //public void SignalLightingManagerPurged(Vector3 _orbPos)
    //{
    //    levelManager.SignalMonsterDeath(monsterID, narrativeID);
    //    LightManager.Instance.Illuminate(monsterID, _orbPos);
    //}

    //protected void ResetStrafe()
    //{
    //    ResetModel();
    //    animManager.SetBool("Strafing", false);
    //    //agent.updateRotation = true;
    //    //rigidbody.freezeRotation = false;
    //    resettingStrafe = false;
    //    forceLateralStrafe = false;
    //    forceBackStrafe = false;
    //    state = State.CHASE; /* maybe throw a choice function here */
    //}

    //private void ResetDisengage()
    //{
    //    animManager.SetBool("Disengaging", false);
    //    resettingDisengage = false;
    //    forceLateralDisengage = false;
    //    forceBackDisengage = false;
    //    state = State.CHASE; /* Add random decision here (between attack idle or strafe perhaps) */
    //}

    //protected virtual void ResetTakingDamage()
    //{
    //    animManager.SetBool("TakingDamage", false);
    //}

    //protected virtual void ResetAttack()
    //{
    //    alreadyAttacked = false;
    //    agent.isStopped = false;
    //}

 
    //private void ResetRandomDisengage()
    //{
    //    canRandomDisengage = true;
    //}

    //protected void ResetRandomStrafe()
    //{
    //    canRandomStrafe = true;
    //}

    //protected void EnableHandEffects()
    //{
    //    if(hasHandEffects)
    //    {
    //        foreach (RFX4_EffectSettings effect in handEffects)
    //        {
    //            effect.IsVisible = true;
    //        }
    //    }
    //}

    //protected void DisableHandEffects()
    //{
    //    if(hasHandEffects)
    //    {
    //        foreach (RFX4_EffectSettings effect in handEffects)
    //        {
    //            effect.IsVisible = false;
    //        }
    //    }
    //}

    //protected void TakeMonsterOutOfLightDamage()
    //{
    //    animManager.speed = 1.0f;
    //    cancelForce = true;

    //    if(!alreadyAttacked)
    //    {
    //        agent.isStopped = false;

    //    }
    //}


    ///* Player effects on monster */
    //public void Root(float _rootTime)
    //{
    //    /* Anim Updates */
    //    animManager.SetBool("Chasing", false);
    //    animManager.SetBool("Strafing", false);
    //    animManager.SetBool("Patroling", false);
    //    animManager.SetBool("Approaching", false);
    //    animManager.SetBool("Disengaging", false);
    //    animManager.SetBool("Attacking", false);
    //    animManager.SetBool("Idling", false);

    //    animManager.SetBool("Rooted", true);

    //    cancelForce = true;
    //    rooted = true;
    //    DisableMonsterMovement();
    //    state = State.ROOTED;

    //    Invoke(nameof(ResetRoot), _rootTime);
    //}

    //protected void ResetRoot()
    //{
    //    EnableMonsterMovement();
    //    rooted = false;
    //    animManager.SetBool("Rooted", false);
    //}

    //protected void DisableMonsterMovement()
    //{
    //    agent.autoBraking = false;
    //    agent.updatePosition = false;
    //    agent.updateRotation = false;
    //    rigidbody.isKinematic = false;
    //    rigidbody.freezeRotation = true;
    //    agent.isStopped = true;
    //    agent.ResetPath();

    //    agent.velocity = new Vector3(0, 0, 0);
    //    rigidbody.velocity = new Vector3(0, 0, 0);

    //    movementDisabled = true;

    //}

    //protected void EnableMonsterMovement()
    //{
    //    rigidbody.isKinematic = true;
    //    rigidbody.freezeRotation = false;
    //    agent.autoBraking = true;
    //    forceApplied = false;
    //    agent.updatePosition = true;
    //    agent.updateRotation = true;
    //    agent.velocity = new Vector3(0, 0, 0);
    //    rigidbody.velocity = new Vector3(0, 0, 0);
    //    agent.isStopped = false;
    //    cancelForce = false;

    //    movementDisabled = false;

    //}

    //protected void ModelFacePlayer()
    //{
    //    if(model != null)
    //    {
    //        var rotation = model.transform.rotation.eulerAngles;
    //        rotation.y = 180;
    //        model.transform.localRotation = Quaternion.Euler(rotation);
    //    }

    //}

    //protected void ResetModel()
    //{
    //    if(model != null)
    //    {
    //        var rotation = model.transform.rotation.eulerAngles;
    //        rotation.y = 0;
    //        model.transform.localRotation = Quaternion.Euler(rotation);
    //    }

    //}

    //public bool CheckIfRooted()
    //{
    //    return rooted;
    //}
}
