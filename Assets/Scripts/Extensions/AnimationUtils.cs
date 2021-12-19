using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationUtils 
{
    public static Coroutine LerpAnimation(this MonoBehaviour behaviuor,float start,float end,float time,Action<float> action)
    {
      return behaviuor.StartCoroutine(LerpCoroutine(start, end, time, action));
    }
    public static IEnumerator LerpCoroutine(float start, float end, float _time, Action<float> action)
    {
        var delta = end - start;
        var LerpTime = Mathf.Abs(delta) * _time;
        var time = Time.time + LerpTime;
        var currenValue = start;
        var progress = 0f;
         while (time>=Time.time)
        {
             progress += Time.deltaTime / LerpTime;
            currenValue =Mathf.Lerp(start, end, progress);
            action(currenValue);
            yield return null;
        }
        yield return null;
        action(end);
    }
}
