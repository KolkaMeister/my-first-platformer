using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker
{
    protected List<Object> lockers = new List<Object>();

    public List<Object> Lockers => lockers;

    public bool IsLocked => lockers.Count > 0;
    public event System.Action<bool> onChange;
    public virtual void Retain(Object _object)
    {
        if (lockers.Contains(_object)) return;
        lockers.Add(_object);
        onChange?.Invoke(IsLocked);
    }
    public virtual void Dispose(Object _object)
    {
        lockers.Remove(_object);
        onChange?.Invoke(IsLocked);
    }
}
