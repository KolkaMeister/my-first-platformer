using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightsConditionContoller : MonoBehaviour
{
    [SerializeField] private List<Light2D> lights = new List<Light2D>();

    public void SetColor(Color color)
    {
        foreach (var light in lights)
        {
            light.color = color;
        }
    }

}
