using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DamageLocker : MonoBehaviour
{
    [SerializeField] private HealthComponent health;
    [SerializeField] private float delay;


    public void Lock()
    {
        StartCoroutine(LockCoroutine());
    }
    public IEnumerator LockCoroutine()
    {
        health.locker.Retain(this);
        yield return new WaitForSeconds(delay);
        health.locker.Dispose(this);


    }


}
