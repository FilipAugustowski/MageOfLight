using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBoolOnEnter : StateMachineBehaviour
{
    public string boolName;
    public bool valueOnEntrance;

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(boolName, valueOnEntrance);
    }
}
