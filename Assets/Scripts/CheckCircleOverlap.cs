using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events ;

public class CheckCircleOverlap : MonoBehaviour
{
    [SerializeField] private float radius = 0.5f;
    [SerializeField] OnOverlapEvent onOverlap;
    [SerializeField] string[] tags;
    [SerializeField] LayerMask layer;
    private Collider2D[] overlapResultTag = new Collider2D[10];
    private Collider2D[] overlapResultLayer = new Collider2D[1];

    public void CheckByTag()
    {
        var size = Physics2D.OverlapCircleNonAlloc(transform.position, radius, overlapResultTag);
        for (int i = 0; i < size; i++)
        {
            var collision = overlapResultTag[i];
            var IsNeededTag = tags.Any(tag => collision.CompareTag(tag));
            if (IsNeededTag)
            {
                onOverlap?.Invoke(collision.gameObject);
            }
        }
    }
    public void CheckByLayer()
    {
        var size = Physics2D.OverlapCircleNonAlloc(transform.position, radius, overlapResultLayer, layer);
        for (int i = 0; i < size; i++)
        {
            var collision = overlapResultLayer[i];
            onOverlap?.Invoke(collision.gameObject);
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = HandlesUtils.TransparentRed;
        Handles.DrawSolidDisc(transform.position, Vector3.forward, radius);
    }
#endif
    [Serializable]
    public class OnOverlapEvent:UnityEvent<GameObject>
    {

    }
}
