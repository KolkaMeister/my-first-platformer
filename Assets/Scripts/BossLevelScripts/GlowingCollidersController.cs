using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlowingCollidersController : MonoBehaviour
{
    [SerializeField] private GlowingCollider[] glowingColliders;

    [ContextMenu("Activate")]
    public void Activate()
    {
        foreach (var item in glowingColliders)
        {
            item.Activate();
        }
    }
    [ContextMenu("Disable")]
    public void Disable()
    {
        foreach (var item in glowingColliders)
        {
            item.Disable();
        }
    }
}


