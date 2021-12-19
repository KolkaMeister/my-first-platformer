using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingComponent : MonoBehaviour
{
    [SerializeField] private Vector3 swingValue;
    [SerializeField] private float swingSpeed;
    [SerializeField] private bool randomizeSpeed;
    [Header("RandomSpeedBorders")]
    [SerializeField] private float min;
    [SerializeField] private float max;
    [Space]
    private Vector3 originalPos;
    private Vector3 targetPos;
    private Vector3 StartPos;
    private void Start()
    {
        originalPos = transform.localPosition;
        SetTargetPos();
        if (randomizeSpeed)
            swingSpeed = Random.Range(min, max);
    }
    private void Update()
    {
        
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, swingSpeed);
        if (transform.localPosition == targetPos)
            SetTargetPos();

    }
    private void SetTargetPos()
    {
        StartPos = transform.localPosition;
        var XAmplitude = swingValue.x/2;
        var YAmplitude = swingValue.y/2;

        var XposDelta = Random.Range(-XAmplitude, XAmplitude);
        var YposDelta= Random.Range(-YAmplitude, YAmplitude);

        targetPos = new Vector3(originalPos.x + XposDelta, originalPos.y + YposDelta, 0);

    }
}
