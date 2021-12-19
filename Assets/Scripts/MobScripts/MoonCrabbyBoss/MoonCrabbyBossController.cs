using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoonCrabbyBossController : MonoBehaviour
{
    [SerializeField] private GameObject Claws;
    [SerializeField] private float enableClawsTime;
    [SerializeField] private ProjectileLauncher projectileLauncher;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator spikeTrapAnimator;
    [SerializeField] private UnityEvent onFightStarted;
    [SerializeField] private UnityEvent onFightEnd;
    [SerializeField] private HealthComponent health;
    [SerializeField] private StartDialogComponent dieDialog;
    private static readonly int IsFighting = Animator.StringToHash("IsFighting");
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Fight = Animator.StringToHash("Fight");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private int maxHealth;




    private void Start()
    {
        health.onChange.AddListener(OnHPChanged);
    }
    private void OnDestroy()
    {
        health.onChange.RemoveListener(OnHPChanged);
    }
    private void OnHPChanged(int _value)
    {
        if (health.Health<=0) return;
        if (health.oldValue-_value>= 20)
        {
            _animator.SetTrigger(Hit);
        }
    }
    [ContextMenu("ClawAttack")]
    public void ClawsAttack()
    {
        StartCoroutine(ClawsAttackCoroutine());
    }
     public IEnumerator ClawsAttackCoroutine()
    {
        Claws.SetActive(true);
        yield return new WaitForSeconds(enableClawsTime);
        Claws.SetActive(false);
    }
    public void ProjectileAttack()
    {
        projectileLauncher.StartLaunching();
    }
    [ContextMenu("StartFight")]
    public void StartFight()
    {
        projectileLauncher.target = FindObjectOfType<Hero>().gameObject;
        _animator.SetBool(IsFighting,true);
        spikeTrapAnimator.SetBool(Fight, true);
        onFightStarted?.Invoke();
    }
    public void OnDie()
    {
        _animator.SetTrigger(Die);
        FightEnd();
        StartAfterDieDiolog();
    }
    public void FightEnd()
    {
        
        _animator.SetBool(IsFighting, false);
        spikeTrapAnimator.SetBool(Fight, false);
        onFightEnd?.Invoke();
    }
    public void StartAfterDieDiolog()
    {
        StartCoroutine(DieDialog());
    }
    public IEnumerator DieDialog()
    {
        yield return new WaitForSeconds(1);
        dieDialog.StartDialog();
    }
}
