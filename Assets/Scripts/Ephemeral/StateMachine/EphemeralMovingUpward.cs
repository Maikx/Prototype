using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EphemeralMovingUpward : StateMachineBehaviour
{
    public EphemeralBehavior ephemeral = null;
    public Vector2 newPos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!ephemeral) ephemeral = animator.gameObject.GetComponent<EphemeralBehavior>();
        newPos = new Vector2(ephemeral.transform.position.x, ephemeral.transform.position.y + ephemeral.moveAmount);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float step = ephemeral.vSpeed * Time.deltaTime;
        ephemeral.transform.position = Vector2.MoveTowards(ephemeral.transform.position, newPos, step);

        if (new Vector2(ephemeral.transform.position.x, ephemeral.transform.position.y) == newPos)
            ephemeral.ephemeralController.SetTrigger("Stop");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
