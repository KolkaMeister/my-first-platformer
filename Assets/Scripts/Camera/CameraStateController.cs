using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraStateController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CinemachineVirtualCamera showCamera;
    private static readonly int ShowTargetKey = Animator.StringToHash("ShowTarget");
    public void SetCameraPosition(Vector3 targetPosition)
    {
        targetPosition.z = showCamera.transform.position.z;
        showCamera.transform.position = targetPosition;
    }
    public void SetState(bool state)
    {
        animator.SetBool(ShowTargetKey, state);
    }

}
