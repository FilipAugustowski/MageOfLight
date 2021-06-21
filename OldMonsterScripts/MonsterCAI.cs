using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCAI : MonsterAI
{
    ///* ID 0-2 is melee attack in animator, 3 is ranged attack */

    //float rangedAttackCD = 13.0f;
    //bool rangedAttackOnCD = false;
    //bool forceRangedAttack = false; /* Used in attack function in order to enable ranged */

    //public MeleeCollider leftHandCollider;
    //public MeleeCollider rightHandCollider;

    //public Transform rightHand; /* For spell cast */

    //public GameObject demonAttackPrefab;
    //public GameObject rangedAttackPrefab;
    //public GameObject aoeAttackPrefab;

    //protected override void Start()
    //{
    //    base.Start();
    //    randomStrafeTimer = 15.0f;
    //    strafeTimeMin = 2.0f;
    //    strafeTimeMax = 3.5f;

    //    //leftHandCollider.Setup(whatIsPlayer, 15, 5);
    //    //rightHandCollider.Setup(whatIsPlayer, 25, 12);

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
    //    if (distanceToWalkPoint.magnitude <= agent.stoppingDistance + 1.0f)
    //        walkPointSet = false;

    //    if (CheckIfPlayerInChaseRange())
    //    {
    //        AudioManager.Instance.Play("BossApproach");
    //        animManager.SetBool("Patroling", false);
    //        patrolling = false;
    //        LvlManager.Instance.NotifyInCombat();
    //        state = State.CHASE;
    //    }


    //}

    //override protected void ChasingPlayer()
    //{
    //    animManager.SetBool("Chasing", true); /* dont really need this to be called every frame */
    //    if (!hasEncounteredPlayer)
    //    {
    //        hasEncounteredPlayer = true;
    //    }

    //    if (CheckIfPlayerInRange(20) && !CheckIfPlayerInAttackRange() && !alreadyAttacked && !rangedAttackOnCD) /* Demon Ranged Attack */
    //    {
    //        animManager.SetBool("Chasing", false);
    //        forceRangedAttack = true;
    //        state = State.ATTACK;
    //    }

    //    else if (RandomStrafe())
    //    {
    //        animManager.SetBool("Chasing", false);
    //        forceLateralStrafe = true;
    //        state = State.STRAFE;
    //    }
    //    else if(CheckIfPlayerInRange(10) && !alreadyAttacked) /* Jump into combo attack */
    //    {
    //        ChooseMeleeAttack(0, true);
    //        animManager.SetBool("Chasing", false);
    //        state = State.ATTACK;
    //    }

    //    else if (CheckIfPlayerInApproachRange())
    //    {
    //        animManager.SetBool("Chasing", false);
    //        state = State.APPROACHINGFORATTACK;
    //    }
    //    else if (CheckIfPlayerInAttackRange())
    //    {
    //        animManager.SetBool("Chasing", false);
    //        state = State.ATTACK;
    //    }
    //    else if(hasEncounteredPlayer && !CheckIfPlayerInChaseRange())
    //    {
    //        ChooseMeleeAttack(0, true);
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

    //    if (RandomStrafe() && player.GetComponent<AnimatorController>().CheckIfPlayerPreppingAttack())
    //    {
    //        animManager.SetBool("Approaching", false);
    //        forceLateralStrafe = true;
    //        AudioManager.Instance.Play("MonsterCStrafe");
    //        state = State.STRAFE;
    //    }

    //    if (RandomDisengage(50))
    //    {
    //        animManager.SetBool("Approaching", false);
    //        forceLateralDisengage = true;
    //        state = State.DISENGAGE;
    //    }

    //    if (!CheckIfPlayerInApproachRange())
    //    {
    //        animManager.SetBool("Approaching", false);
    //        state = State.CHASE;
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
    //        if (forceRangedAttack && !rangedAttackOnCD)
    //        {
    //            EnableHandEffects();
    //            animManager.SetInteger("AttackID", 4);
    //            animManager.SetBool("Attacking", true); /* Choose anim attack */
    //            alreadyAttacked = true;
    //            forceRangedAttack = false;
    //            rangedAttackOnCD = true;
    //            DisableMonsterMovement();

    //            Invoke(nameof(EnableMonsterMovement), 1.0f);

    //            Invoke(nameof(ResetRangedAttack), rangedAttackCD);

    //            AudioManager.Instance.Play("MonsterCApproach");
    //            /* Attacking is reset in the animation */
    //            Invoke(nameof(ResetAttack), timeBetweenAttacks);
    //        }
    //        else
    //        {
    //            /* Choose between attack 0,1,2 */
    //            EnableHandEffects();
    //            ChooseMeleeAttack();
    //            animManager.SetBool("Attacking", true); /* Choose anim attack */
    //            AudioManager.Instance.Play("MonsterCAttack");
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
    //                forceLateralStrafe = true;
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
    //    if (_heavyHit)
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
    //            Invoke(nameof(TakeMonsterOutOfLightDamage), 0.5f);

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

    //            if (patrolling)
    //            {
    //                animManager.SetBool("Patroling", false);
    //                patrolling = false;
    //                state = State.CHASE;
    //            }
    //        }
    //    }

    //}

    //protected override void Die()
    //{
    //    base.Die();
    //    AudioManager.Instance.Play("BossKilled");
    //    model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y - 2, model.transform.localPosition.z);
    //}

    //protected override bool RandomStrafe()
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

    //#region MonsterC Special Abilties 

    //public void JumpTowardsPlayer()
    //{
    //    if (animManager.GetBool("Attacking"))
    //    {

    //        transform.LookAt(player);

    //        /* Want them to go towards the player */
    //        Vector3 direction = (player.transform.position - transform.position).normalized;
    //        float attackForce = 13.0f;

    //        StartCoroutine(ApplyForceAsVelocity(direction, 1.3f, attackForce));
    //    }
    //}

    //public override void ApplyDisengageForce()
    //{
    //    transform.LookAt(player);
    //    StartCoroutine(ApplyForceAsVelocity(disengageDirection, 1.3f, disengageForce));
    //}

    //public void SpawnRangedAttack() /* Called from animation */
    //{
    //    /* prefab spawn and send at player with specfic speed */
    //    GameObject rangedAttack = Instantiate(rangedAttackPrefab, rightHand.position, Quaternion.identity);
    //    rangedAttack.GetComponent<MonsterBRangedAttack>().BeginTravel(GetDirectionTowardsPlayer());
    //}

    //public void SpawnDemonAttack()
    //{
    //    GameObject demonAttack = Instantiate(demonAttackPrefab, rightHandCollider.transform.position, this.transform.rotation);
    //    demonAttack.GetComponent<MonsterCDemonAttack>().BeginTravel(GetDirectionTowardsPlayer());
    //}

    //public void SpawnAoeAttack()
    //{
    //    float x = transform.position.x;
    //    float y = transform.position.y;
    //    float z = transform.position.z + 1 * transform.forward.z;

    //    Vector3 position = new Vector3(x, y, z);

    //    GameObject aoeAttack = Instantiate(aoeAttackPrefab, position, Quaternion.identity);

    //}

    //private void ChooseMeleeAttack(int x_ = 0, bool _forceAttack = false)
    //{
    //    int x = Random.Range(0, 4); /* The demon attack is excluded from this list */

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

    ////public void EnableRightHandCollider()
    ////{
    ////    rightHandCollider.EnableCollider();
    ////}

    ////public void DisableRightHandCollider()
    ////{
    ////    rightHandCollider.DisableCollider();

    ////}

    ////public void EnableLeftHandCollider()
    ////{
    ////    leftHandCollider.EnableCollider();
    ////}

    ////public void DisableLeftHandCollider()
    ////{
    ////    leftHandCollider.DisableCollider();

    ////}

    //#endregion
}
