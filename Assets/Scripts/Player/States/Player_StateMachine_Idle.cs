using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StateMachine_Idle : StateMachineBehaviour
{
    PlayerController pC = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!pC) pC = animator.gameObject.GetComponent<PlayerController>();

        pC.currentSpeed = 0;
        pC.currentGravity = 0;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!pC.isDead)
            pC.currentGravity = pC.gravity;
    }
}
