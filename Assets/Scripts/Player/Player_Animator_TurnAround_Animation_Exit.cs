using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animator_TurnAround_Animation_Exit : StateMachineBehaviour
{
    PlayerController pC = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!pC) pC = animator.gameObject.GetComponentInParent<PlayerController>();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pC.RotateModel();
    }
}
