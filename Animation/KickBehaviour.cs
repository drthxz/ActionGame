using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickBehaviour : StateMachineBehaviour
{
    // Start is called before the first frame update
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetComponentInChildren<Damage>()!=null){
            animator.GetComponentInChildren<Damage>().Kick(true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.GetComponentInChildren<Damage>().Kick(false);
    }
}
