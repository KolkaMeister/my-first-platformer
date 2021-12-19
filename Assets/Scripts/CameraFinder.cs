using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFinder : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private Camera _camera;
    void Start()
    {
        _camera = FindObjectOfType<Camera>();
        canvas.worldCamera = _camera;
    }


}
