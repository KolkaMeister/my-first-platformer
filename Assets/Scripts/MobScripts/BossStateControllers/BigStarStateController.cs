using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BigStarStateController : MonoBehaviour
{
    [SerializeField] private HealthComponent health;
    [SerializeField] private Animator animator;
    [SerializeField] private UnityEvent onFightStarted;
    [SerializeField] public CircularShootComponent circularShoot;
    [Header("Flood")]
    [SerializeField] private Animator floodAnimator;
    [SerializeField] private float floodDelay;
    [SerializeField] private LightsConditionContoller LightsController;
    private int maxHealth;
    private bool fightStarted;

    private static readonly int HP = Animator.StringToHash("HP");
    private static readonly int START = Animator.StringToHash("START");
    private static readonly int IsFlooding = Animator.StringToHash("IsFlooding");
    private static readonly int Die = Animator.StringToHash("Die");
    private void Start()
    {
        maxHealth = health.Health;
        health.onHpChanged += OnHealthChange;

        animator.SetFloat(HP, maxHealth / maxHealth);
    }

    public void OnHealthChange(int _value)
    {
        if (!fightStarted) StartFight();
        animator.SetTrigger(START);
        animator.SetFloat(HP, (float)_value / maxHealth);
    }
    public void StartFight()
    {
        animator.SetTrigger(START);
        onFightStarted?.Invoke();
        fightStarted = true;
    }

    public void OnDestroy()
    {
        health.onHpChanged -= OnHealthChange;
    }
    public void Flood()
    {
        StartCoroutine(FloodColorutine());
    }
    public IEnumerator FloodColorutine()
    {
        floodAnimator.SetBool(IsFlooding, true);
        yield return new WaitForSeconds(floodDelay);
        floodAnimator.SetBool(IsFlooding, false);

    }
    public void OnDie()
    {
        animator.SetTrigger(Die);
        LightsController.SetColor(Color.white);
    }
    public void ChangeLightsColor()
    {
        LightsController.SetColor(Color.red);
    }
}
