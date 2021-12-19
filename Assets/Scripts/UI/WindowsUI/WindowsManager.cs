using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsManager : AnimatedWindow
{

    protected override void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Open()
    {
        animator.SetBool(IsOpened, true);
    }
    public override void Close()
    {
        base.Close();
    }
}
