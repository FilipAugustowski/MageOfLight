using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBAI : MonsterAI
{
    ///* ID 0-2 is melee attack in animator, 3 is ranged attack */

    //float rangedAttackCD = 10.0f;
    //bool rangedAttackOnCD = false;
    //bool forceRangedAttack = false; /* Used in attack function in order to enable ranged */

    ////public MeleeCollider leftHandCollider;
    ////public MeleeCollider rightHandCollider;

    //public GameObject rangedAttackPrefab;
    //public GameObject aoeAttackPrefab;

    //protected override void Start()
    //{
    //    base.Start();
    //    randomStrafeTimer = 15.0f;
    //}

    //protected override void Patroling()
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
    //    if (distanceToWalkPoint.magnitude <= agent.stoppingDistance + 3.0f)
    //        walkPointSet = false;

    //    if (CheckIfPlayerInChaseRange())
    //    {
    //        AudioManager.Instance.Play("MonsterBNoticePlayer");
    //        animManager.SetBool("Patroling", false);
    //        patrolling = false;
    //        LvlManager.Instance.NotifyInCombat();
    //        state = State.CHASE;
    //    }
        
        
    //}

    //override protected void ChasingPlayer()
    //{
    //    animManager.SetBool("Chasing", true); /* dont really need this to be called every frame */
    //    agent.speed = chaseSpeed;

    //    if (!hasEncounteredPlayer)
    //    {
    //        hasEncounteredPlayer = true;

    //    }
    //    if (CheckIfPlayerInRange(20) && !alreadyAttacked)
    //    {
    //        animManager.SetBool("Approaching", false);
    //        forceRangedAttack = true;
    //        state = State.ATTACK;
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

    //    agent.isStopped = false;
    //    agent.speed = chaseSpeed;

    //    NavMesh.CalculatePath(transform.position, PlayerTracker.Instance.transform.position, agent.areaMask, path);
    //    agent.SetPath(path);
    //}


    //protected override void ApproachingForAttack()
    //{
    //    animManager.SetBool("Approaching", true);

    //    transform.LookAt(player);
    //    agent.speed = approachSpeed;

    //    NavMesh.CalculatePath(transform.position, PlayerTracker.Instance.transform.position, agent.areaMask, path);
    //    agent.SetPath(path);

    //    if (CheckIfPlayerInAttackRange() && !alreadyAttacked)
    //    {
    //        animManager.SetBool("Approaching", false);
    //        state = State.ATTACK;
    //    }

    //    if (CheckIfPlayerInRange(20) && !alreadyAttacked && !rangedAttackOnCD)
    //    {
    //        animManager.SetBool("Approaching", false);
    //        forceRangedAttack = true;
    //        state = State.ATTACK;
    //    }

    //    if (CheckIfPlayerInAttackRange() && alreadyAttacked)
    //    {
    //        animManager.SetBool("Approaching", false);
    //        state = State.IDLING;
    //    }

    //}


    //protected override void AttackingPlayer()
    //{
    //    /* Anim Updates */
    //    animManager.SetBool("Chasing", false);
    //    animManager.SetBool("Patroling", false);
    //    animManager.SetBool("Approaching", false);

    //    transform.LookAt(player);

    //    /* Collision Detection is done on the animation itself */

    //    if (!alreadyAttacked)
    //    {
    //        if (forceRangedAttack)
    //        {
    //            EnableHandEffects();
    //            animManager.SetInteger("AttackID", 3);
    //            animManager.SetBool("Attacking", true); /* Choose anim attack */
    //            alreadyAttacked = true;
    //            forceRangedAttack = false;
    //            agent.isStopped = true;
    //            rangedAttackOnCD = true;
    //            Invoke(nameof(ResetRangedAttack), rangedAttackCD);

    //            /* Attacking is reset in the animation */
    //            Invoke(nameof(ResetAttack), timeBetweenAttacks);
    //        }
    //        else
    //        {
    //            /* Choose between attack 0,1,2 */
    //            EnableHandEffects();
    //            ChooseMeleeAttack();
    //            animManager.SetBool("Attacking", true); /* Choose anim attack */
    //            AudioManager.Instance.Play("MonsterBAttack");
    //            alreadyAttacked = true;


    //            Invoke(nameof(ResetAttack), timeBetweenAttacks); /* Attack animation boolean is reset in anim manager */
    //        }
    //    }
    //    else
    //    {
    //        if (!animManager.GetBool("Attacking"))
    //        {
    //            DisableHandEffects();
    //            if (RandomStrafe())
    //            {
    //                //animManager.SetBool("Approaching", false);
    //                //forceLateralStrafe = true;
    //                state = State.STRAFE;
    //            }
    //            else
    //            {
    //                state = State.APPROACHINGFORATTACK;
    //            }

    //        }

    //    }
    //}

    //public override void TakeDamage(int _damage, float _hitForce, bool _heavyHit = false)
    //{
    //    if(_heavyHit)
    //    {
    //        base.TakeDamage(_damage, _hitForce);
    //    }
    //    else
    //    {
    //        if (alive)
    //        {
    //            health -= _damage;
    //            animManager.speed = 0;
    //            agent.isStopped = true;
    //            /* play hit sound */
    //            ApplyForceAsVelocity(GetDirectionAwayFromPlayer(), 0.3f);
    //            Invoke(nameof(TakeMonsterOutOfLightDamage), 0.5f) ;

    //            if (health <= 0)
    //            {
    //                alive = false;
    //                animManager.SetBool("Chasing", false);
    //                animManager.SetBool("Strafing", false);
    //                animManager.SetBool("Patroling", false);
    //                animManager.SetBool("Approaching", false);
    //                animManager.SetBool("Disengaging", false);
    //                animManager.SetBool("Attacking", false);
    //                animManager.SetBool("Idling", false);
    //                animManager.SetBool("TakingDamage", false);
    //                animManager.SetBool("Dying", true);
    //                state = State.IDLING;
    //                Invoke(nameof(Die), deathTimer);
    //            }

    //            if(patrolling)
    //            {
    //                animManager.SetBool("Patroling", false);
    //                patrolling = false;
    //                state = State.CHASE;
    //            }
    //        }
    //    }
        
    //}

    //#region MonsterC Special Abilties 
    //public void SpawnRangedAttack() /* Called from animation */
    //{
    //    /* prefab spawn and send at player with specfic speed */
    //    GameObject rangedAttack = Instantiate(rangedAttackPrefab, rightHandCollider.transform.position, Quaternion.identity);
    //    rangedAttack.GetComponent<MonsterBRangedAttack>().BeginTravel(GetDirectionTowardsPlayer());
    //}

    //public void SpawnAoeAttack()
    //{
    //    float x = transform.position.x;
    //    float y = transform.position.y;
    //    float z = transform.position.z + 1 * transform.forward.z;

    //    Vector3 position = new Vector3(x, y, z);

    //    GameObject aoeAttack = Instantiate(aoeAttackPrefab, position, Quaternion.identity);

    //}

    //private void ChooseMeleeAttack()
    //{
    //    int x = Random.Range(0, 3);

    //    animManager.SetInteger("AttackID", x);
    //}

    //#endregion

    //#region Called from Animations or Animator 
    //public void ApplySmallAttackForce()
    //{
    //    if (animManager.GetBool("Attacking"))
    //    {
    //        transform.LookAt(player);

    //        /* Want them to go towards the player */
    //        Vector3 direction = (player.transform.position - transform.position).normalized;
    //        float attackForce = 6.0f;

    //        StartCoroutine(ApplyForceAsVelocity(direction, 1.3f, attackForce));
    //    }
    //}

    //public void ApplyMediumAttackForce()
    //{
    //    if (animManager.GetBool("Attacking"))
    //    {

    //        transform.LookAt(player);

    //        /* Want them to go towards the player */
    //        Vector3 direction = (player.transform.position - transform.position).normalized;
    //        float attackForce = 9.0f;

    //        StartCoroutine(ApplyForceAsVelocity(direction, 1.3f, attackForce));
    //    }
    //}

    //public void ApplyLargeAttackForce()
    //{
    //    if (animManager.GetBool("Attacking"))
    //    {

    //        transform.LookAt(player);

    //        /* Want them to go towards the player */
    //        Vector3 direction = (player.transform.position - transform.position).normalized;
    //        float attackForce = 12.0f;

    //        StartCoroutine(ApplyForceAsVelocity(direction, 1.3f, attackForce));
    //    }
    //}



    //private void ResetRangedAttack()
    //{
    //    rangedAttackOnCD = false;

    //}

    //public void EnableRightHandCollider()
    //{
    //    //rightHandCollider.EnableCollider();
    //}

    //public void DisableRightHandCollider()
    //{
    //    //rightHandCollider.DisableCollider();

    //}

    //public void EnableLeftHandCollider()
    //{
    //    //leftHandCollider.EnableCollider();
    //}

    //public void DisableLeftHandCollider()
    //{
    //    //leftHandCollider.DisableCollider();

    //}

    //#endregion
}
