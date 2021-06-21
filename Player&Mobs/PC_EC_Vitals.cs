using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_EC_Vitals : MonoBehaviour
{
    public float currentKnockback = 0;
    public float knockbackResetTimer = 0;
    public float knockbackResetTime = 1.5f;

    public string[] damageSounds;

    public bool canBeHit;

    /* knockback heavyMax 
     * knockback lightMax
     * hit anim to play index
     */

    public float health = 100;
    public int maxHealth = 100;

    protected float mana;
    protected int maxMana;
    protected float manaRegen;

    public float stamina;
    public int maxStamina;
    protected float staminaRegen;

    /* 3 Types of Hit Animations: light, medium, heavy */
    public int lightHitForceReqForAnim = 15;
    public int mediumHitForceReqForAnim = 30;
    public int heavyHitForceReqForAnim = 50;

    public PC_EC_Vitals whoDealtDamageToMeLast;

    void Start()
    {
        mana = 100;
        maxMana = 100;
        manaRegen = 10.0f;

        //stamina = 100;
        //maxStamina = 100;
        staminaRegen = 10.0f;

        canBeHit = true;

        Init();
    }

    virtual protected void Init()
    {

    }

    public virtual void HandleDamage(float _damage, int _force, PC_EC_Vitals _whoDealtDamageToMeLast, bool _playSound = true)
    {
        if(canBeHit)
        {
            whoDealtDamageToMeLast = _whoDealtDamageToMeLast;

            if (_playSound)
            {

                if(damageSounds.Length > 0)
                {
                    int x = damageSounds.Length;

                    int random = Random.Range(0, x);

                    AudioManager.Instance.Play(damageSounds[random]);

                }
                else
                {
                    AudioManager.Instance.Play("PC_EC_TakeDamage");
                }
            }


            if (knockbackResetTimer <= 0) currentKnockback = 0;

            knockbackResetTimer = knockbackResetTime;
            currentKnockback += _force;
            DeductHealth(_damage);

            /* In the override functions the player or enemy will play an animation for getting hit 
             * based off of their currentKnockBack */
        }
    }

    public void HandleKnockBackTimer()
    {
        knockbackResetTimer -= Time.deltaTime;
    }


    #region Health
    public void HealthRegen(float _regenAmount)
    {
        if (health < maxHealth)
        {
            health += _regenAmount;
        }
        else
        {
            health = maxHealth;
        }

        SendHealthToUI();

    }

    public void DeductHealth(float _damage)
    {
        health -= _damage;
        SendHealthToUI();
    }

    virtual protected void SendHealthToUI()
    {

    }


    public void ResetHealth()
    {
        health = 100;
        SendHealthToUI();
    }

    #endregion

    #region Mana

    public void DeductMana(float _manaCost)
    {
        mana -= _manaCost;

        if (mana < 0) mana = 0;

        SendManaToUI();

    }

    public void ManaRegen(float _regenAmount)
    {
        if (mana < maxMana)
        {
            mana += _regenAmount;
        }
        else
        {
            mana = maxMana;
        }

        SendManaToUI();

    }

    public float GetCurrentMana()
    {
        return mana;
    }

    public void ResetMana()
    {
        mana = 100;
        SendManaToUI();
    }

    virtual protected void SendManaToUI()
    {
    }
    #endregion

    #region Stamina
    public void DeductStamina(float _staminaCost)
    {
        stamina -= _staminaCost;

        if (stamina < 0) stamina = 0;

        SendStaminaToUI();

    }

    public void StaminaRegen(float _regenAmount)
    {
        if (stamina < maxStamina)
        {
            stamina += _regenAmount;
        }
        else
        {
            stamina = maxStamina;
        }

        SendStaminaToUI();

    }

    public float GetCurrentStamina()
    {
        return stamina;
    }

    public void ResetStamina()
    {
        mana = 100;
        SendStaminaToUI();
    }

    virtual protected void SendStaminaToUI()
    {
    }
    #endregion
}
