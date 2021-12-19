using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlowingPlatformComponent : MonoBehaviour
{
    [SerializeField] private float maxGlowItensity;
    [SerializeField] private float minGlowItensity;
    private Light2D _light;

    private Coroutine lepAnimCoRoutine;
    private void Start()
    {
        _light = GetComponent<Light2D>();
    }

    private void Update()
    {
        
    }
}
