using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PC_PlayerVitals : PC_EC_Vitals
{
    [HideInInspector]
    public PC_PlayerManager playerManager;
    
    PC_AnimatorController animatorController;
    PC_SpellHandler spellHandler;
    public LvlManager level;


    override protected void Init()
    {
        PC_UIManager.Instance.SetupHealth(maxHealth);
        PC_UIManager.Instance.SetupStamina(maxStamina);
        PC_UIManager.Instance.SetupMana(maxMana);
        playerManager = GetComponent<PC_PlayerManager>();
        animatorController = GetComponentInChildren<PC_AnimatorController>();
        spellHandler = GetComponent<PC_SpellHandler>();
        level = (LvlManager)FindObjectOfType(typeof(LvlManager));
    }

    public void RegenStats() /* may need to add this later to PC_EC_Vitals */
    {
        if (!playerManager.isInteracting)
        {
            ManaRegen(manaRegen * Time.deltaTime);
            if(!playerManager.isSprinting) StaminaRegen(staminaRegen * Time.deltaTime);
        }
    }

    override protected void SendHealthToUI()
    {
        PC_UIManager.Instance.UpdateHealth(health);
    }

    override protected void SendManaToUI()
    {
        PC_UIManager.Instance.UpdateMana(mana);
    }

    override protected void SendStaminaToUI()
    {
        PC_UIManager.Instance.UpdateStamina(stamina);
    }

    public override void HandleDamage(float _damage, int _force, PC_EC_Vitals _whoDealtDamageToMeLast, bool _playSound = true)
    {
        
        base.HandleDamage(_damage, _force, _whoDealtDamageToMeLast, _playSound);


        animatorController.DeactivateMeleeCollider();


        if (!playerManager.isDead && canBeHit)
        {
            if (health <= 0)
            {
                spellHandler.StopPlayerCasting();
                currentKnockback = 0;
                animatorController.PlayTargetAnimation("PC_Death", true, 1);
                PC_UIManager.Instance.CameraFadeIn();

                /* Load death scene */
                Debug.Log("Should load death scene here ");
                Invoke(nameof(ReloadLevel), 5.0f);
                
            }
            else if (currentKnockback >= mediumHitForceReqForAnim)
            {
                animatorController.PlayTargetAnimation("PC_HitMedium", true, 1);
                spellHandler.StopPlayerCasting();
            }
            else if (currentKnockback >= lightHitForceReqForAnim)
            {
                animatorController.PlayTargetAnimation("PC_HitLight", true, 1);
                spellHandler.StopPlayerCasting();
            }

        }
    }

    private void ReloadLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        level.RespawnMobs();
        transform.position = level.respawn + new Vector3(0, 1.0f, 0);
        transform.eulerAngles = level.newRot;
        ResetHealth();
        playerManager.isDead = false;
        PC_UIManager.Instance.CameraFadeOut();
        animatorController.StopCurrentAnimation();
        playerManager.isInAir = false;
        playerManager.isGrounded = true;
        animatorController.animator.SetBool("IsInAir", false);



    }

}
