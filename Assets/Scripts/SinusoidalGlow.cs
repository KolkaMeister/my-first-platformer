using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class SinusoidalGlow : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    [SerializeField] private bool randomize;
    private Light2D _light;
    private float currentPhase;
    private float defaultOuterRadius;
    private void Start()
    {
        if(randomize)currentPhase = Random.value;
        _light = GetComponent<Light2D>();
        defaultOuterRadius = _light.pointLightOuterRadius;
    }

    private void Update()
    {
        currentPhase += Time.deltaTime;
        _light.pointLightOuterRadius = defaultOuterRadius + Mathf.Abs(Mathf.Sin(currentPhase*frequency)*amplitude);
    }


}
