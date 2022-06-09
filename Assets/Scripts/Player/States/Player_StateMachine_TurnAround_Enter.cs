using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StateMachine_TurnAround_Enter : StateMachineBehaviour
{
    PlayerController pC = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!pC) pC = animator.gameObject.GetComponent<PlayerController>();
        animator.SetBool("Delay", true);
        pC.currentDelayTime = pC.timeToTurnAround;
    }
}
