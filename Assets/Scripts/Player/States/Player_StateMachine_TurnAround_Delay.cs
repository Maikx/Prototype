using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StateMachine_TurnAround_Delay : StateMachineBehaviour
{
    PlayerController pC = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!pC) pC = animator.gameObject.GetComponent<PlayerController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (pC.currentTurnAroundTime > 0)
        {
            pC.canMoveTurnAround = false;
            pC.currentTurnAroundTime -= Time.deltaTime;
        }
        else if (pC.currentTurnAroundTime <= 0)
        {
            animator.SetBool("Delay", false);
            pC.canMoveTurnAround = true;
        }
    }
}
