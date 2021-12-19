using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BallOfDeath : MonoBehaviour
{
    [SerializeField]  private float lerpTime;
    [SerializeField] public BallOfDeathController controller;
    private Light2D _light;
    private float originalItensity;

    private Coroutine lerpCoroutine;
    private void Start()
    {
        _light = GetComponent<Light2D>();
        this.LerpAnimation( 0, _light.intensity, lerpTime, SetItensity);
        originalItensity = _light.intensity;
    }
    public void Disappear()
    {
        controller.health.ApplyDamage(controller.damage);
        controller.Spawn();
        StartCoroutine(DestroyCoroutine());
    }
    private IEnumerator DestroyCoroutine()
    {
        this.LerpAnimation(_light.intensity, 0, lerpTime, SetItensity);
        yield return new WaitForSeconds(lerpTime);
        Destroy(gameObject);
    }
    private void SetItensity(float _value)
    {
        _light.intensity = _value;
    }
}
