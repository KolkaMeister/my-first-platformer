﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroBehaviour : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<BigStarStateController>().ChangeLightsColor();
    }


}
