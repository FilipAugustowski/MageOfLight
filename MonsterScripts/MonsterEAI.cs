using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterEAI : MonsterAI
{
    ////protected LvlManager levelManager;
    ////public int monsterID = -1; /* If this monster does not have narratives or does not activate a light group than make this ID -1 */

    ///* Narrative Stuff */
    ////protected int narrativeID;
    ////protected bool hasEncounteredPlayer = false;

    ////public NavMeshAgent agent;

    ////protected Transform player;

    ////public LayerMask whatIsGround, whatIsPlayer;

    ////public GameObject deathOrbPrefab;
    //bool instantiated = false; /* Used to start the FSM for the monster */
    //Rigidbody rigidbody;
    //public GameObject spawnSpot;
    //public float far;
    //public Vector3 playerpos;
    ///*
    //public new enum State
    //{
    //    WAIT,
    //    ATTACKING,
    //    TAKEDAMAGE,
    //    DIE,
        
    //}
    //*/
    ///* State machine vars*/
    ////public new State state;


    ///* Attacking */
    //public int attackDmg = 15;
    //public new bool alreadyAttacked;
    //public float numAttacks = 3f;
    //public float attackCd = 1.5f;
    //public float attackDelay = 6f;
    ////public float attackRange;


    ////public float health;

    //public bool isAlive;
    //public bool randomCurves = false;
    //public float curveHeight = 10f;
    //public float curveWidth = -0.5f;
    //public GameObject fireball;
    //public GameObject player1;
   
  
    ///*
    //void Start()
    //{
    //    //player = PlayerTracker.Instance.transform;
    //    levelManager = LvlManager.Instance;
    //    isAlive = true;
    //}
    //*/
    //protected override void Awake()
    //{
    //    player1 = GameObject.FindGameObjectWithTag("Player");
    //    agent = GetComponent<NavMeshAgent>();
    //    rigidbody = GetComponent<Rigidbody>();
     
    //    alive = true;
    //    StartCoroutine("ForcedAttack");
    //}

    //protected override void Patroling()
    //{
    //    transform.LookAt(player);
    //    //agent.destination = transform.position;
    //    if (CheckIfPlayerInAttackRange())
    //    {
    //        //LvlManager.Instance.NotifyInCombat();
    //        state = State.ATTACK;
    //    }
    //}
    
    //// Update is called once per frame
    ///*
    //void LateUpdate()
    //{
    //    if (!instantiated)
    //    {
    //        state = State.WAIT;
    //        StartCoroutine("FSM");
    //        instantiated = true;
    //    }
    //    playerpos = player1.transform.position;

    //}
    ///*
    //IEnumerator FSM()
    //{
  
    //    while (isAlive)
    //    {
    //        switch (state)
    //        {
    //            case State.WAIT:
    //                Waiting();
    //                break;
    //            case State.ATTACKING:
    //                AttackingPlayer();
    //                yield return new WaitForSeconds(4f);
    //                break;
    //            case State.TAKEDAMAGE:

    //                break;


    //        }
    //        yield return null;
    //    }
    //}
    //protected void Waiting()
    //{
    //    Debug.Log("I am waiting");
    //    transform.LookAt(player);
    //    //agent.destination = transform.position;
    //    if (CheckIfPlayerInAttackRange())
    //    {
    //        //LvlManager.Instance.NotifyInCombat();
    //        state = State.ATTACKING;
    //    }

    //}
    //*/
    ///**
    //public virtual void TakeDamage(int _damage, float _hitForce, bool _heavyHit = false)
    //{
    //    if (isAlive)
    //    {
    //         Anim Updates 
   
    //        //animManager.SetBool("Attacking", false);
    //        //animManager.SetBool("Waiting", false);
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
    ////}

    ////animManager.SetBool("TakingDamage", true);

    ////enteringTakeDamage = true;
    ////tempHitforce = _hitForce;
    ////state = State.TAKINGDAMAGE; /* State switch here */

    ///* Taking damage animation is reset in animation */
    ////            }
    ////     }
    //// } 
    //protected override void Idling()
    //{
    //    //Debug.Log("Yo im idle");
    //    /* purge all animations */
    //    animManager.SetBool("Idling", true);
    //    transform.LookAt(player);


    //    //idleTimer += Time.deltaTime;

    //    if (CheckIfPlayerInAttackRange())
    //    {
    //        animManager.SetBool("Idling", false);
    //        state = State.ATTACK;
    //    }

 
    //}


    //protected override void AttackingPlayer()
    //{

    //   // SpawnRangedAttack();
    //    if (!alreadyAttacked)
    //    {
    //        alreadyAttacked = true;
    //        StartCoroutine("BarrageAttack");

    //    }
    //    if (!CheckIfPlayerInAttackRange())
    //    {
    //        state = State.IDLING;
    //    }
    //}
    //IEnumerator BarrageAttack()
    //{
    //    for(int i = 0; i < numAttacks; i++)
    //    {
    //        SpawnRangedAttack();
    //        yield return new WaitForSeconds(attackCd);
    //    }
    //    Invoke("ResetAttack", attackDelay);

    //    yield return null;
    //}
    //protected override void ResetAttack()
    //{
    //    alreadyAttacked = false;
    //    //agent.isStopped = false;
    //}
    
    //protected override bool CheckIfPlayerInAttackRange()
    //{
    //    //Debug.Log("test");
    //    far = Vector3.Distance(transform.position, player1.transform.position);
    //    return (far <= attackRange);
    //    //return Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
    //}
    
    //protected Vector3 GetDirectionTowardsPlayer()
    //{
    //    return (player.transform.position - transform.position).normalized;
    //}
    
    //public void SpawnRangedAttack() /* Called from animation */
    //{
    //    /* prefab spawn and send at player with specfic speed */

    //    GameObject rangedAttack = Instantiate(fireball, transform.position, Quaternion.identity);
    //    if (randomCurves)
    //    {
            
    //    }
    //    else
    //    {
    //        rangedAttack.GetComponent<MonsterERange>().BeginTravel(spawnSpot.transform.position, player1.transform.position, curveHeight);

    //    }
    //}

    //IEnumerator ForcedAttack()
    //{
    //    while (true)
    //    {
    //        if (CheckIfPlayerInAttackRange())
    //        {
    //            state = State.ATTACK;
    //        }
    //        if(state == State.ATTACK)
    //        {
    //            AttackingPlayer();
    //        }

    //        yield return null;
    //    }

    //}

}
