using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTargerComponent : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private CameraStateController controller;
    private void OnValidate()
    {
        if (controller==null)
        {
            controller = FindObjectOfType<CameraStateController>();
        }
    }
    public void ShowTarget()
    {
        controller.SetCameraPosition(target.position);
        controller.SetState(true);

    }
    public void MoveBack()
    {
        controller.SetState(false);
    }
}
