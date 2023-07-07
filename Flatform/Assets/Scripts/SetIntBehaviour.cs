using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIntBehaviour : StateMachineBehaviour
{
    public string intName;
    public bool updateOnStateEnter, updateOnStateExit;
    public bool updateOnStateMachineEnter, updateOnStateMachineExit;
    public int valueOnEnter, valueOnExit, valueOnMin, valueOnMax;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateEnter)
        {
            animator.SetInteger(intName, valueOnEnter);
        }   
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateExit)
        {
            valueOnExit = Random.Range(valueOnMin, valueOnMax);
            animator.SetInteger(intName, valueOnExit);
        }   
    }

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineEnter)
        {
            animator.SetInteger(intName, valueOnEnter);    
        }
    }

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineExit)
        {
            animator.SetInteger(intName, valueOnExit);
        }
    }
}
