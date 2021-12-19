using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchComponent : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animationKey ;
    [SerializeField] private bool state;

    private void Start()
    {
        animator.SetBool(animationKey, state);
    }
    public void SetTrue()
    {
        state = true;
        animator.SetBool(animationKey, state);
    }
    public void SetFalse()
    {
        state = false;
        animator.SetBool(animationKey, state);
    }
    public void Switch()
    {
        if (state==true)
        {
            state = false;
        }
        else if (state == false)
        {
            state = true;
        }  

        animator.SetBool(animationKey, state);
    }
    [ContextMenu("Switch")]
    public void SwitchIt()
    {
        Switch();
    }
}
