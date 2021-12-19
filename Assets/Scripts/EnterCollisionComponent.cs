using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnterCollisionComponent : MonoBehaviour
{
    [SerializeField] private string tag;
        [SerializeField] LayerMask layer=~0;
    [SerializeField] private EnterEvent action;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.IsInLayer(layer)) return;
        if (!string.IsNullOrEmpty(tag) && !other.gameObject.CompareTag(tag)) return;
        action?.Invoke(other.gameObject);

    }
    [Serializable]
    public class EnterEvent : UnityEvent<GameObject>
    {
    }
}
