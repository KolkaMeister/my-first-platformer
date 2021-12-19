using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class LightColorLerp : MonoBehaviour
{


    private Light2D _light;

    private Coroutine lerpCoroutine;
    private void Start()
    {
        _light = GetComponent<Light2D>();
    }

    private void StartLerpColorRoutine()
    {
      //  lerpCoroutine = StartCoroutine(LerpColor());
    }    

    private IEnumerator LerpColor(Color start,Color end)
    {
        float _progress=0;
        while(_progress<1)
        {
            Color.Lerp(start, end, _progress);
            yield return null;
        }
    }
}
