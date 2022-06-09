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
        if (pC.currentDelayTime > 0)
        {
            pC.canMoveScript = false;
            pC.currentDelayTime -= Time.deltaTime;
        }
        else if (pC.currentDelayTime <= 0)
        {
            animator.SetBool("Delay", false);
            pC.canMoveScript = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.Rotate(animator.gameObject.transform.rotation.x, 180, animator.gameObject.transform.rotation.z);
    }
}
