using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastLayerCheck : LayerCheck
{
    [SerializeField] private Transform target;
    private RaycastHit2D[] result = new RaycastHit2D[1];

    private void Update()
    {
        isTouchingLayer = Physics2D.RaycastNonAlloc(transform.position, target.position, result, layer)>0;
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.DrawLine(transform.position, target.position);
    }
#endif
}
