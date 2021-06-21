using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_AnimatorController : PC_EC_AnimatorController
{
    EC_EnemyManager enemyManager;

    public PC_EC_MeleeCollider leftWeaponCollider;
    public PC_EC_MeleeCollider rightWeaponCollider;
    public EC_Spell currSpell;
    public Transform spellSpawnPos;


    /* Easiest To Place this information here for our AI */
    public float currDamage;
    public int currForce;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyManager = GetComponentInParent<EC_EnemyManager>();
    }

    private void OnAnimatorMove() /* recenters model on gameobject during root motion */
    {
        if (enemyManager.gamePaused) return;
        float delta = Time.deltaTime;
        enemyManager.rigidbody.drag = 0;
        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        enemyManager.rigidbody.velocity = velocity; /* might need to add move speed */
    }

    public void ActivateMeleeColliderLeft()
    {
        leftWeaponCollider.currDamage = currDamage;
        leftWeaponCollider.currForce = currForce;
        leftWeaponCollider.EnableDamageCollider();
    }

    public void ActivateMeleeColliderRight()
    {
        rightWeaponCollider.currDamage = currDamage;
        rightWeaponCollider.currForce = currForce;
        rightWeaponCollider.EnableDamageCollider();
    }

    public void DeactivateMeleeColliderLeft()
    {
        leftWeaponCollider.currDamage = currDamage;
        leftWeaponCollider.currForce = currForce;
        leftWeaponCollider.DisableDamageCollider();
    }

    public void DeactivateMeleeColliderRight()
    {
        rightWeaponCollider.currDamage = currDamage;
        rightWeaponCollider.currForce = currForce;
        rightWeaponCollider.DisableDamageCollider();
    }

    public void ActivateSpell()
    {
        if(currSpell)
        {
            currSpell.ActivateSpellObject(spellSpawnPos.position);
        }
    }
    public void StopCurrentAnimation()
    {
        animator.SetBool("IsInteracting", false);
        animator.CrossFade("Empty", 0.2f, 1);

    }
}
