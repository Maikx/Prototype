using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StateMachine_Bark_Delay : StateMachineBehaviour
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
}
