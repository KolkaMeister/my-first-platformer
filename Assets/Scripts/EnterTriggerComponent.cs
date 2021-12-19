using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnterTriggerComponent : MonoBehaviour
{
    [SerializeField] string tag;
    [SerializeField] LayerMask layer=~0;
    [SerializeField] private EnterEvent action;
    private void OnTriggerEnter2D(Collider2D other)
    {
         if (!other.gameObject.IsInLayer(layer)) return;
        if (!string.IsNullOrEmpty(tag) &&!other.gameObject.CompareTag(tag)) return;
        action?.Invoke(other.gameObject);
        
    }
    [Serializable]
    public class EnterEvent : UnityEvent<GameObject>
    {
    }
}
