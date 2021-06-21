using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PC_EC_AnimatorController : MonoBehaviour
{
    public Animator animator;
    public string lastStatePlayedByTargeting;


    public void PlayTargetAnimation(string _stateName, bool _isInteracting, int _layer)
    {
        animator.applyRootMotion = _isInteracting;
        animator.SetBool("IsInteracting", _isInteracting);
        animator.CrossFade(_stateName, 0.2f, _layer);
        lastStatePlayedByTargeting = _stateName;
    }
}
