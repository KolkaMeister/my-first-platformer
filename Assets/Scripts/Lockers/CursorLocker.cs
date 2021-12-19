using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLocker : Locker
{

    public override void Retain(Object _object)
    {
        if (lockers.Contains(_object)) return;
        lockers.Add(_object);
        Check();
    }
    public override void Dispose(Object _object)
    {
        base.Dispose(_object);
        Check();
    }
    public void Check()
    {
        if (lockers.Count>0)
        {
            Cursor.lockState=CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }
    }
}
