using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class StarProjectile : Projectile
{
    [SerializeField] private float destroyTime;
    [SerializeField] private float lerpAnimTime;
    [SerializeField] private SinusoidalGlow sinGlow;
    private Light2D _light;
    private float endItensty;

    private Coroutine lerpCoroutine;
    protected override void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _light = GetComponent<Light2D>();
        endItensty = _light.intensity;
        lerpCoroutine=this.LerpAnimation(0, endItensty, lerpAnimTime, SetItensity);
    }
    public void SetItensity(float progress)
    {
        _light.intensity = progress;
    }

    public void DestroyAnimation()
    {
        if (lerpCoroutine != null) StopCoroutine(lerpCoroutine);
        lerpCoroutine = this.LerpAnimation(_light.intensity, 0, destroyTime, SetItensity);
        StartCoroutine(WaitAndDestroy());
    }
    public IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
