using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlowingCollider : MonoBehaviour
{
    [SerializeField] public Light2D _light;
    [SerializeField] public Collider2D _collider;
    [SerializeField] private float stateChangeTime;
    [SerializeField] private bool activated;
    [SerializeField] private bool disableOnStart;
    public float originalItensity;
    private Coroutine stateChangeCoroutine;
    private Coroutine lerpAnimCoroutine;
    private void Start()
    {
        originalItensity = _light.intensity;
        if (disableOnStart)
        {
            activated = true;
            Disable();
        }
    }
    [ContextMenu("Activate")]
    public void Activate()
    {
        if (activated) return;
        if (stateChangeCoroutine != null) StopCoroutine(stateChangeCoroutine);
        stateChangeCoroutine=StartCoroutine(ActivateCoroutine());
    }
    [ContextMenu("Disable")]
    public void Disable()
    {
        if (!activated) return;
        if (stateChangeCoroutine != null) StopCoroutine(stateChangeCoroutine);
        stateChangeCoroutine = StartCoroutine(DisableCoroutine());
    }
    private IEnumerator DisableCoroutine()
    {
        StartLerpAnim(originalItensity,0);
        activated = false;
        yield return new WaitForSeconds(stateChangeTime);
        _collider.enabled = false;
    }
    private IEnumerator ActivateCoroutine()
    {
        StartLerpAnim(0,originalItensity);
        activated = true;
        yield return new WaitForSeconds(stateChangeTime);
        _collider.enabled = true;
    }


    private void StartLerpAnim(float start,float end)
    {
        if (lerpAnimCoroutine != null) StopCoroutine(lerpAnimCoroutine);
        lerpAnimCoroutine = this.LerpAnimation(start, end, stateChangeTime, SetItensity);
    }
    public void SetItensity(float progress)
    {
        _light.intensity = progress;
    }
}
