using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_SpellHandler : MonoBehaviour
{
    PC_PlayerManager playerManager;
    PC_AnimatorController animatorController;
    PC_PlayerVitals playerVitals;
    PC_Spell currSpell;
    public List<PC_Spell> spells;
    int spellIndex;

    public Transform spellSpawnTransform;

    float timeCasting;
    float quickCastTimeWindow;
    bool canQuickCast;
    bool canNormalCast;

    //float inputDelayTimer;
    //public float castInputDelay = 1.0f;


    void Start()
    {
        playerManager = GetComponent<PC_PlayerManager>();
        playerVitals = GetComponent<PC_PlayerVitals>();
        animatorController = GetComponentInChildren<PC_AnimatorController>();
        
        if(spells.Count > 0) currSpell = spells[0];

        //inputDelayTimer = 0;

        spellIndex = 0;
        timeCasting = 0;
        quickCastTimeWindow = .15f;
        canQuickCast = false;
        canNormalCast = false;
    }

    public void AddSpell(PC_Spell _spell)
    {
        quickCastTimeWindow = .15f;
        currSpell = _spell;
        spells.Add(_spell);
        if (CurrAbilityIcon.Instance)
        {
            CurrAbilityIcon.Instance.UpdateCurrentSpellSprite();
        }
    }

    public void HandleSpellSwap(bool _swapInput)
    {
        if(!playerManager.isInteracting)
        {
            if(_swapInput)
            {
                if (spellIndex + 1 < spells.Count)
                {
                    spellIndex++;
                    currSpell = spells[spellIndex];

                    if(CurrAbilityIcon.Instance)
                    {
                        CurrAbilityIcon.Instance.UpdateCurrentSpellSprite();
                    }

                }
                else
                {
                    spellIndex = 0;
                    currSpell = spells[spellIndex];

                    if (CurrAbilityIcon.Instance)
                    {
                        CurrAbilityIcon.Instance.UpdateCurrentSpellSprite();
                    }
                }
            }

        }
    }

    public void HandleCasting(bool _casted, float _delta)
    {

        if(_casted && 
            !playerManager.isTakingDamage &&
            spells.Count > 0 &&
            !playerManager.isSprinting)
        { 
            if(!playerManager.isInCombatMode)
            {
                animatorController.PlayTargetAnimation("PC_DrawSword", true, 2);
                AudioManager.Instance.Play("PC_Sheathe");
                playerManager.isInCombatMode = true;
            }
            else if(!playerManager.isInteracting)
            {
                if(currSpell.CheckIfQuickCastable(playerVitals.GetCurrentMana()))
                {
                    canQuickCast = true;
                }
                else
                {
                    if (currSpell.hasQuickCast && !playerManager.isCasting)
                    {
                        //inputDelayTimer = castInputDelay;
                        HandleNotEnoughMana();
                        return;
                    }
                }
                timeCasting += _delta;
                
                if(timeCasting >= quickCastTimeWindow)
                {
                    if(currSpell.CheckIfNormalCastable(playerVitals.GetCurrentMana()))
                    {
                        canNormalCast = true;
                        HandleNormalCast();
                    }

                }
            }
        }
        else
        {
            if(canNormalCast) /* If the player taps left click and the spell has a quickcast option than quickcast it */
            {
                //inputDelayTimer = castInputDelay;

                canNormalCast = false;
                canQuickCast = false;
                timeCasting = 0;
                animatorController.StopCurrentAnimation();
            }
            else if(canQuickCast) /* If the player is charging the spell and releases then stop the animation */
            {
                //inputDelayTimer = castInputDelay;

                canQuickCast = false;
                canNormalCast = false;
                timeCasting = 0;
                HandleQuickCast();
            }
            else
            {
                canNormalCast = false;
                canQuickCast = false;
                timeCasting = 0;
            }

        }


        //HandleInputDelay();

    }

    //void HandleInputDelay()
    //{
    //    if(inputDelayTimer > 0)
    //    {
    //        inputDelayTimer -= Time.deltaTime;
    //    }
    //    if(inputDelayTimer < 0)
    //    {
    //        inputDelayTimer = 0;
    //    }
    //}
    
    public void StopPlayerCasting()
    {
        canNormalCast = false;
        canQuickCast = false;
        timeCasting = 0;
        animatorController.StopCurrentAnimation();
    }

    public void HandleQuickCast()
    {
        animatorController.animator.SetBool("Casting", true);
        animatorController.PlayTargetAnimation(currSpell.quickCastAnimation, true, currSpell.quickCastAnimLayer);
    }
    public void HandleNormalCast()
    {
        /* Mana is deduced by the normal casting animation */
        animatorController.PlayTargetAnimation(currSpell.castingAnimation, true, currSpell.castingAnimLayer);
    }

    public void HandleNotEnoughMana()
    {
        animatorController.PlayTargetAnimation("PC_OutOfMana", true, 2);
        PC_UIManager.Instance.DisplayTooltip("Not Enough Mana", true, 3.0f);
    }

    //public bool CheckIfEnoughManaQuickCast()
    //{
    //    Debug.Log("Current mana: " + playerVitals.GetCurrentMana());
    //    Debug.Log("Mana cost: " + currSpell.quickManaCost);
    //    if (playerVitals.GetCurrentMana() >= currSpell.quickManaCost) return true;
    //    else return false;
    //}

    public bool CheckIfEnoughManaNormalCast()
    {
        if (currSpell.normalManaCost <= playerVitals.GetCurrentMana()) return true;
        else return false;
    }

    public bool CheckCurrSpellQuickCast()
    {
        return currSpell.hasQuickCast;
    }
    

    /* These is called from the animator and it tells the current spell
     * to activate its funtionality */

    /* Deduct mana in these functions */
    public void ActivateCurrSpellQuickCastObject()
    {
        playerVitals.DeductMana(currSpell.quickManaCost);
        currSpell.ActivateQuickCastSpell(spellSpawnTransform.position);
    }
    public void ActivateCurrSpellNormalCastObject()
    {
        playerVitals.DeductMana(currSpell.normalManaCost);
        currSpell.ActivateNormalCastSpell(spellSpawnTransform.position);
    }

    public Sprite GetCurrentSpellSprite()
    {
        return currSpell.spellSprite;
    }

}
