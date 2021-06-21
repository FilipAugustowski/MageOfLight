using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* https://www.youtube.com/watch?v=R6vtF4mpiOw&list=PLD_vBJjpCwJtrHIW1SS5_BNRk6KZJZ7_d&index=13&ab_channel=SebastianGraves */
public class PC_MeleeHandler : MonoBehaviour
{
    PC_AnimatorController animatorController;
    PC_PlayerManager playerManager;
    PC_PlayerVitals playerVitals;

    public PC_EC_MeleeCollider swordCollider;

    public PC_MeleeAttack[] lightCombo0;
    public PC_MeleeAttack[] lightCombo1;

    public PC_MeleeAttack[] heavyCombo0;
    public PC_MeleeAttack[] heavyCombo1;

    string lastAttack;
    int currComboIndex;
    bool doCombo;

    float meleeChargeTimer;

    bool canLightAttack;
    bool canHeavyAttack;

    /* Stamina Is removed from the melee animations
     * in the case that a player gets interupted before
     * they do damage/when they start the attack */
    float lightAttackStaminaCost;
    float heavyAttackStaminaCost;
    float lightAttackDamage;
    float heavyAttackDamage;
    int lightAttackForce;
    int heavyAttackForce;


    void Start()
    {
        swordCollider = GetComponentInChildren<PC_EC_MeleeCollider>();
        playerVitals = GetComponent<PC_PlayerVitals>();
        playerManager = GetComponent<PC_PlayerManager>();
        animatorController = GetComponentInChildren<PC_AnimatorController>();
        currComboIndex = 0;
        meleeChargeTimer = 0;
        lightAttackStaminaCost = 15f;
        heavyAttackStaminaCost = 20f;
        lightAttackDamage = 10f;
        heavyAttackDamage = 25f;
        lightAttackForce = 15;
        heavyAttackForce = 25;
        canLightAttack = false;
        canHeavyAttack = false;
        doCombo = false;
    }

    /* Receive a true or false input bool from the input handler
     * If the player isnt doing other stuff than allow them to light attack
     * or quickly charge a heavy attack */
    public void HandleAttack(bool _attacked, float _delta)
    {
        if (_attacked)
        {
            if(!playerManager.isInteracting)
            {
                if(playerManager.isInCombatMode)
                {
                    playerManager.isAttacking = true;
                    meleeChargeTimer += _delta;
                    canLightAttack = true;

                    if (meleeChargeTimer >= 0.15)
                    {
                        canHeavyAttack = true;
                    }
                }
                else
                {
                    animatorController.PlayTargetAnimation("PC_DrawSword", true, 2);
                    AudioManager.Instance.Play("PC_Sheathe");
                    playerManager.isInCombatMode = true;
                }
            }
            else if(animatorController.GetCanDoCombo())
            {
                doCombo = true;
            }

        }
        else
        {
            if (doCombo)
            {
                doCombo = false;
                meleeChargeTimer = 0;
                canLightAttack = false;
                canHeavyAttack = false;

                int random = Random.Range(0, 11);

                if(random > 5)
                {
                    AudioManager.Instance.Play("PC_Sword_0");
                }
                else
                {
                    AudioManager.Instance.Play("PC_Sword_1");

                }


                HandleCombo();
            }
            else if (canHeavyAttack)
            {
                meleeChargeTimer = 0;
                canLightAttack = false;
                canHeavyAttack = false;



                HandleHeavyAttack();
            }
            else if (canLightAttack)
            {

                meleeChargeTimer = 0;
                canLightAttack = false;
                canHeavyAttack = false;


                HandleLightAttack();
            }
            else
            {
                /* Sometimes animations can be canceled and we want to be
                 * sure that the melee collider is turned off */

                //DeactivateMeleeCollider(); 
                playerManager.isAttacking = false;
                meleeChargeTimer = 0;
                canLightAttack = false;
                canHeavyAttack = false;
            }
        }


        /* Reset Attack Colliders if an animation was interupted */
        if(!playerManager.isInteracting)
        {
            swordCollider.DisableDamageCollider();
        }


    }

    public void HandleLightAttack()
    {
        if (playerVitals.GetCurrentStamina() >= lightAttackStaminaCost)
        {
            animatorController.PlayTargetAnimation(lightCombo1[0].animationName, true, 1);
            currComboIndex = 0;
            lastAttack = lightCombo1[0].animationName;

            int random = Random.Range(0, 11);

            if (random > 5)
            {
                AudioManager.Instance.Play("PC_Sword_0");
            }
            else
            {
                AudioManager.Instance.Play("PC_Sword_1");

            }

            /* Used to choose a random combo, but it doesnt feel right */
            //int random = Random.Range(0, 2);
            //currComboIndex = 0;

            //if (random == 0)
            //{
            //    animatorController.PlayTargetAnimation(lightCombo0[0].animationName, true, 1);
            //    lastAttack = lightCombo0[0].animationName;
            //}
            //else
            //{
            //    animatorController.PlayTargetAnimation(lightCombo1[0].animationName, true, 1);
            //    lastAttack = lightCombo1[0].animationName;
            //}
        }
        else
        {
            PC_UIManager.Instance.DisplayTooltip("Not Enough Stamina", true, 3.0f);

        }
    }

    public void HandleHeavyAttack()
    {
        if(playerVitals.GetCurrentStamina() >= heavyAttackStaminaCost)
        {

            currComboIndex = 0;
            animatorController.PlayTargetAnimation(heavyCombo1[0].animationName, true, 1);
            lastAttack = heavyCombo1[0].animationName;

            int random = Random.Range(0, 11);

            if (random > 5)
            {
                AudioManager.Instance.Play("PC_Sword_0");
            }
            else
            {
                AudioManager.Instance.Play("PC_Sword_1");

            }

            //int random = Random.Range(0, 2);
            //currComboIndex = 0;


            //if (random == 0)
            //{
            //    animatorController.PlayTargetAnimation(heavyCombo0[0].animationName, true, 1);
            //    lastAttack = heavyCombo0[0].animationName;
            //}
            //else
            //{
            //    animatorController.PlayTargetAnimation(heavyCombo1[0].animationName, true, 1);
            //    lastAttack = heavyCombo1[0].animationName;
            //}
        }
        else
        {
            PC_UIManager.Instance.DisplayTooltip("Not Enough Stamina", true, 3.0f);

        }

    }

    public void HandleCombo()
    {
        animatorController.DisableCombo();

        #region Combos

        #region Light Combo Variant 0
        if (lastAttack == lightCombo0[0].animationName)
        {
            currComboIndex++;
            animatorController.PlayTargetAnimation(lightCombo0[currComboIndex].animationName, true, 1);
            lastAttack = lightCombo0[currComboIndex].animationName;

        }
        else if (lastAttack == lightCombo0[currComboIndex].animationName)
        {
            currComboIndex++;
            animatorController.PlayTargetAnimation(lightCombo0[currComboIndex].animationName, true, 1);
            lastAttack = lightCombo0[currComboIndex].animationName;

        }
        #endregion

        #region Light Combo Variant 1
        else if (lastAttack == lightCombo1[0].animationName)
        {
            currComboIndex++;
            animatorController.PlayTargetAnimation(lightCombo1[currComboIndex].animationName, true, 1);
            lastAttack = lightCombo1[currComboIndex].animationName;
        }
        else if (lastAttack == lightCombo1[currComboIndex].animationName)
        {
            currComboIndex++;
            animatorController.PlayTargetAnimation(lightCombo1[currComboIndex].animationName, true, 1);
            lastAttack = lightCombo1[currComboIndex].animationName;

        }
        #endregion

        #region Heavy Combo Variant 0
        else if (lastAttack == heavyCombo0[0].animationName)
        {
            currComboIndex++;
            animatorController.PlayTargetAnimation(heavyCombo0[currComboIndex].animationName, true, 1);
            lastAttack = heavyCombo0[currComboIndex].animationName;
        }
        else if (lastAttack == heavyCombo0[currComboIndex].animationName)
        {
            currComboIndex++;
            animatorController.PlayTargetAnimation(heavyCombo0[currComboIndex].animationName, true, 1);
            lastAttack = heavyCombo0[currComboIndex].animationName;

        }
        #endregion

        #region Heavy Combo Variant 1
        else if (lastAttack == heavyCombo1[0].animationName)
        {
            currComboIndex++;
            animatorController.PlayTargetAnimation(heavyCombo1[currComboIndex].animationName, true, 1);
            lastAttack = heavyCombo1[currComboIndex].animationName;
        }
        else if (lastAttack == heavyCombo1[currComboIndex].animationName)
        {
            currComboIndex++;
            animatorController.PlayTargetAnimation(heavyCombo1[currComboIndex].animationName, true, 1);
            lastAttack = heavyCombo1[currComboIndex].animationName;

        }
        #endregion

        #endregion
    }

    public void DeductStaminaLightAttack()
    {
        swordCollider.UpdateDamage(lightAttackDamage);
        swordCollider.UpdateForce(lightAttackForce);
        playerVitals.DeductStamina(lightAttackStaminaCost);
    }

    public void DeductStaminaHeavyAttack()
    {
        swordCollider.UpdateDamage(heavyAttackDamage);
        swordCollider.UpdateForce(heavyAttackForce);
        playerVitals.DeductStamina(heavyAttackStaminaCost);
    }

    public void ActivateMeleeCollider()
    {
        swordCollider.EnableDamageCollider();
    }

    public void DeactivateMeleeCollider()
    {
        swordCollider.DisableDamageCollider();
    }
}
