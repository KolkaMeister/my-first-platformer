using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonCrabbyLaunch : StateMachineBehaviour
{
   
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<MoonCrabbyBossController>().ProjectileAttack();
    }



}
