using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/* Summary

*/


public class PC_AnimatorController : PC_EC_AnimatorController
{
    /* Accesses */
    PC_PlayerManager playerManager;
    PC_InputManager inputManager;
    PC_MovementController movementController;
    PC_SpellHandler spellHandler;
    PC_MeleeHandler meleeHandler;
    PC_PlayerVitals playerVitals;

    int vertical;
    int horizontal;
    public bool canRotate;

    public string[] footstepSounds;

    public void Initialize()
    {
        playerVitals = GetComponentInParent<PC_PlayerVitals>();
        meleeHandler = GetComponentInParent<PC_MeleeHandler>();
        spellHandler = GetComponentInParent<PC_SpellHandler>();
        playerManager = GetComponentInParent<PC_PlayerManager>();
        movementController = GetComponentInParent<PC_MovementController>();
        inputManager = GetComponentInParent<PC_InputManager>();
        animator = GetComponent<Animator>();
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }


    public bool CheckBool(string _boolToCheck)
    {
        return animator.GetBool(_boolToCheck);
    }
   
    /* Only one bool is needed for upper body: isInteracting. If combat = none the play is not interacting, otherwise check all the states and play the appropriate animation */
    /* To do combos, you must let the animations finish or at some point towards the end check in */

    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool _isSprinting)
    {
        
        #region Vertical
        float v = 0;

        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            v = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            v = 1;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            v = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            v = -1;
        }
        else
        {
            v = 0;
        }

        #endregion

        #region Horizontal
        float h = 0;

        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            h = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            h = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            h = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            h = -1;
        }
        else
        {
            h = 0;
        }

        #endregion

        if (_isSprinting)
        {
            v = 2;
            h = horizontalMovement;
        }

        animator.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        animator.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
    }

    void OnAnimatorMove()
    {
        if (playerManager.gamePaused) return;
        if (!playerManager.isInteracting ||
            playerManager.isDashing) return;
        else
        {
            float delta = Time.deltaTime;
            movementController.rigidbody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            movementController.rigidbody.velocity = velocity;
        }
    }

    public void CanRotate()
    {
        canRotate = true;
    }

    public void StopRotation()
    {
        canRotate = false;
    }



    public void StopCurrentAnimation()
    {
        animator.SetBool("IsInteracting", false);
        animator.CrossFade("Empty", 0.2f, 1);
        animator.CrossFade("Empty", 0.2f, 2);

    }

    public bool GetIsInteracting()
    {
        return animator.GetBool("IsInteracting");
    }


    #region Methods Called from animations

    /* Called in order to reset the height of the player's capsule collider, right before
     * the landing part of the animation is played */
    public void EndJumping()
    {
        animator.SetBool("Jumping", false);
        movementController.EndJumping();
    }

    public void ActivateMeleeCollider()
    {
        meleeHandler.ActivateMeleeCollider();
    }

    public void DeactivateMeleeCollider()
    {
        meleeHandler.DeactivateMeleeCollider();
    }

    public void ActivateHandSpellCollider() /* Used only on PC_LightAttack_4 */
    {
        Debug.Log("Hand Spell Activated");
    }

    public void DeactivateHandSpellCollider() /* Used only on PC_LightAttack_4 */
    {
        Debug.Log("Hand Spell deactivated");
    }

    public void DeductStaminaLightAttack()
    {
        meleeHandler.DeductStaminaLightAttack();   
    }

    public void DeductStaminaHeavyAttack()
    {
        meleeHandler.DeductStaminaHeavyAttack();
    }

    public bool GetCanDoCombo()
    {
        return animator.GetBool("CanDoCombo");
    }

    public void EnableCombo()
    {
        animator.SetBool("CanDoCombo", true);
    }

    public void DisableCombo()
    {
        animator.SetBool("CanDoCombo", false);
    }

    public void ActivateCurrSpellNormalCast()
    {
        spellHandler.ActivateCurrSpellNormalCastObject();
    }

    public void ActivateCurrSpellQuickCast()
    {
        spellHandler.ActivateCurrSpellQuickCastObject();
    }

    public void DisableDamage()
    {
        playerVitals.canBeHit = false;
    }

    public void EnableDamage()
    {
        playerVitals.canBeHit = true;
    }

    /* Attaches sword to player's hand */
    public void DrawSword()
    {
        playerManager.swordHolder.sword.transform.parent = playerManager.swordHolder.handParent;

        /* Set the proper rotation and Position for the sword */
        Vector3 correctRotation = new Vector3(8.465f, 101.082f, 5.916f);
        Quaternion newRotation = Quaternion.Euler(correctRotation);
        playerManager.swordHolder.sword.transform.localRotation = newRotation;
        playerManager.swordHolder.sword.transform.localPosition = new Vector3(-0.044f, 0.026f, 0.004f);

    }

    /* Makes the sword a child of the swordholder */
    public void SheatheSword()
    {
        playerManager.swordHolder.sword.transform.parent = playerManager.swordHolder.transform;
        Vector3 correctRotation = new Vector3(346.4075f, 119.8451f, 90.4885f);
        Quaternion newRotation = Quaternion.Euler(correctRotation);
        playerManager.swordHolder.sword.transform.localRotation = newRotation;
        playerManager.swordHolder.sword.transform.localPosition = new Vector3(-0.224f, 0.0568f, 0.2759f);

        /* Set the proper rotation for the sword */

    }

    public void Footstep()
    {
        if(playerManager.isGrounded && !playerManager.isJumping )
        {
            int x = Random.Range(0, 3);

            if (footstepSounds.Length > 0) AudioManager.Instance.Play(footstepSounds[x]);
        }

    }

    #endregion
}
