using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheck : LayerCheck
{
    private Collider2D collider2d;
    private void Awake()
    {
        collider2d = GetComponent<Collider2D>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        isTouchingLayer = collider2d.IsTouchingLayers(layer);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isTouchingLayer = collider2d.IsTouchingLayers(layer);
    }
}
