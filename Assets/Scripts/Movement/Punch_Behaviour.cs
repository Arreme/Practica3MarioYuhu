using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch_Behaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    private bool wasEnabled = true;
    [SerializeField] private float startTime = 0.3f;
    [SerializeField] private float endTime = 0.7f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stateInfo.normalizedTime < startTime && wasEnabled)
        {
            animator.gameObject.GetComponent<PunchController>().setPunchTriggerState(false);
            wasEnabled = false; 
        }
        else if (stateInfo.normalizedTime > startTime && stateInfo.normalizedTime < endTime && !wasEnabled) {
            animator.gameObject.GetComponent<PunchController>().setPunchTriggerState(true);
            wasEnabled = true;
        }
        else if (stateInfo.normalizedTime > endTime && wasEnabled)
        {
            animator.gameObject.GetComponent<PunchController>().setPunchTriggerState(false);
            wasEnabled = false;
        }
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
