using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodBehaviour : StateMachineBehaviour
{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       var controller=animator.GetComponent<BigStarStateController>();
        controller.Flood();
    }


}
