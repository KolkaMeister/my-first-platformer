using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerComponent : MonoBehaviour
{
    [SerializeField] TimerData[] timers;

    public void SetTimer(int index)
    {
        var timer = timers[index];
        StartCoroutine(StartTimer(timer));
    }
    private IEnumerator StartTimer(TimerData timer)
    {
        yield return new WaitForSeconds(timer.delay);
        timer.OnTimesUp.Invoke();
    }

    [Serializable]
    public class TimerData
    {
        public float delay;
        public UnityEvent OnTimesUp;
    }

}
