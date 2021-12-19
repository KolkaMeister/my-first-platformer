using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedWindow : MonoBehaviour
{
    protected Animator animator;
    protected static readonly int IsOpened = Animator.StringToHash("IsOpened");
    protected Action CloseAction;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(IsOpened,true);

    }
    public virtual void Close()
    {
        animator.SetBool(IsOpened, false);
    }
    public virtual void OnCloseAnimationComplete()
    {
        CloseAction?.Invoke();
    }
    public virtual void OnOpenAnimationComplete()
    {
        
    }
}
