using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Camera followCamera;
    [SerializeField] private float multiplayer;

    private Vector3 originPosition;
    private void Start()
    {
        originPosition = transform.position;
    }
    private void Update()
    {
        transform.position = transform.position + followCamera.transform.position * multiplayer;
    }
}
