using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animator_TurnAround_Animation : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.parent.Rotate(animator.gameObject.transform.rotation.x, 180, animator.gameObject.transform.rotation.z);
    }
}
