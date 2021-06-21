using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimatorController : MonoBehaviour
{
    ///* Animations that need to tell scripts their total duration */
    //[HideInInspector]
    //public Animator animManager;

    //MonsterAI monsterAI;

    ///* Specific Animation Information */
    //public string disengageClipName = "MonsterJumpBack";
    //float disengageClipTime;
    //public float disengageClipSpeedMultiplier;

    //public string takingDamageClipName = "Zombie Reaction Hit";
    //float takingDamageClipTime;
    //public float takingDamageClipSpeedMultiplier;


    //public string attacking1ClipName = "Zombie Punching";
    //float attack1ClipTime;
    //public float attacking1ClipSpeedMultiplier;

    //MeleeCollider meleeCollider; /* Add functionality for multiple colliders */


    ////SpecialAnimationInfo disengageInfo;
    ////SpecialAnimationInfo takingDamageInfo;
    ////SpecialAnimationInfo attacking1Info;

    //void Awake()
    //{
    //    animManager = GetComponent<Animator>();
    //    meleeCollider = GetComponentInChildren<MeleeCollider>();
    //    monsterAI = GetComponent<MonsterAI>();
    //    //https://answers.unity.com/questions/692593/get-animation-clip-length-using-animator.html);
    //    /* Should go into monsterAnimatorController */
    //    RuntimeAnimatorController ac = animManager.runtimeAnimatorController;    //Get Animator controller
    //    for (int i = 0; i < ac.animationClips.Length; i++)                 //For all animations
    //    {
    //        if (ac.animationClips[i].name == disengageClipName)        //If it has the same name as your clip
    //        {
    //            disengageClipTime = ac.animationClips[i].length;
    //        }
    //        if (ac.animationClips[i].name == takingDamageClipName)        
    //        {
    //            takingDamageClipTime = ac.animationClips[i].length;
    //        }
    //        if (ac.animationClips[i].name == attacking1ClipName)
    //        {
    //            attack1ClipTime = ac.animationClips[i].length;
    //        }
    //    }

    //}

    //public float GetDisengageAnimationTime()
    //{
    //    return disengageClipTime / disengageClipSpeedMultiplier;
    //}

    //public float GetTakingDamageAnimationTime()
    //{
    //    return takingDamageClipTime / takingDamageClipSpeedMultiplier;
    //}

    //public float GetAttackAnimationTime()
    //{
    //    return attack1ClipTime / attacking1ClipSpeedMultiplier;
    //}

    //public void EnableCollider()
    //{
    //    //meleeCollider.EnableCollider();
    //}

    //public void DisableCollider()
    //{
    // //   meleeCollider.DisableCollider();

    //}

}
