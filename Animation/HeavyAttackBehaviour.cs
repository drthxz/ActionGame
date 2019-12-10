using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackBehaviour : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetComponentInChildren<Damage>()!=null){
            animator.GetComponentInChildren<Damage>().HeavyAttack(true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.GetComponentInChildren<Damage>().HeavyAttack(false);
    }

}
