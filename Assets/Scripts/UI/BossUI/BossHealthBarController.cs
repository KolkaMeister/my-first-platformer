using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBarController : MonoBehaviour
{
    [SerializeField] private HealthComponent health;
    [SerializeField] private ProgressBarWidget progressBar;
    [SerializeField] private CanvasGroup group;
    [SerializeField] private float animTime;
    private int maxHealth;
    private bool possibleToLerp=true;

    private Coroutine lerpAnimationCorouitne;
    public void Start()
    {
        health.onHpChanged += OnHpChanged;
        maxHealth=health.maxHealth;
    }
    public void OnHpChanged(int hp)
    {
        if (hp > 0)
        {
            Show();
        }
        else
        {
            Hide();
        }
        progressBar.SetProgress((float)hp / maxHealth);
        
    }
    private void OnDestroy()
    {
        health.onHpChanged -= OnHpChanged;
    }
    private void Show()
    {
        StartLerpAnimationCoroutine(1);
    }
    private void Hide()
    {
        if (possibleToLerp)
        {
        StartLerpAnimationCoroutine(0);
            possibleToLerp = false;
        }
    }

    private void StartLerpAnimationCoroutine(float endValue)
    {
        if (lerpAnimationCorouitne != null) StopCoroutine(lerpAnimationCorouitne);
        lerpAnimationCorouitne = this.LerpAnimation(group.alpha, endValue, animTime, SetAlpha);
    }
    private void SetAlpha(float progress)
    {
        group.alpha = progress;
    }
}
