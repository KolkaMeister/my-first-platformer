using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolItem : MonoBehaviour
{
    private int id;
    private Pool pool;

    [SerializeField] private UnityEvent OnRelease;

    public void Retain()
    {
        pool.ReturnToPool(id, this);
    }
    public void Release()
    {
        OnRelease?.Invoke();
    }

    public void SetData(int _id,Pool _pool)
    {
        id = _id;
        pool = _pool;
    }
}
