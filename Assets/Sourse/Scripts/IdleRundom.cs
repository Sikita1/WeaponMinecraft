using UnityEngine;

public class IdleRundom : StateMachineBehaviour
{
    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.SetInteger("IdleID", Random.Range(0, 14));
    }
}
