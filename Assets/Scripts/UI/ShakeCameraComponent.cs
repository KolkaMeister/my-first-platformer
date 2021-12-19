using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeCameraComponent : MonoBehaviour
{
    [SerializeField] private float shakeTime;
    [SerializeField] private float itensity;

    private CinemachineVirtualCamera virtualCamera;

    private CinemachineBasicMultiChannelPerlin cameraNoise;

    private Coroutine coroutine;
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void Shake()
    {
        if (coroutine != null)
            StopShake();
       coroutine=StartCoroutine(StartShake());

    }
    public IEnumerator StartShake()
    {
        cameraNoise.m_FrequencyGain = itensity;
        yield return new WaitForSeconds(shakeTime);
        StopShake();
    }
    public void StopShake()
    {
        cameraNoise.m_FrequencyGain = 0;
        StopCoroutine(coroutine);
    }


}
