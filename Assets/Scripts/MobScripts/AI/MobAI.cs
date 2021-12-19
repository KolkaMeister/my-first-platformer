using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    [SerializeField] private ColliderCheck AttackRange;
    [SerializeField] private ColliderCheck Vision;
    [SerializeField] private float AgroDelay = 1f;
    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] private float missDelay = 0.5f;
    [SerializeField] private GamePerson person;
    [SerializeField] SpawnListComponent effects;
    [SerializeField] Patrol patrol;
    [SerializeField] Vector2 deadColliderSize;

    private CapsuleCollider2D collider2d;
    private Animator animator;
    private bool isDead;
    private Coroutine currentCorotine;
    private GameObject target;
    private void Awake()
    {
        collider2d = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        person = GetComponent<GamePerson>();
        effects = GetComponent<SpawnListComponent>();
    }
    private void Start()
    {

        StartState(patrol.DoPatrol());
    }
    private void StartState(IEnumerator coroutine)
    {
        if (currentCorotine!=null)
        {
            StopCoroutine(currentCorotine);
        }
        currentCorotine = StartCoroutine(coroutine);
    }
    public void OnHeroVision(GameObject hero)
    {
        if (isDead) return;
        target = hero;
        StartState(Agro());
    }
    private IEnumerator Agro()
    {
        effects.Spawn("Agro");
        yield return new WaitForSeconds(AgroDelay);
        StartState(GoToTarget());
    }
    private IEnumerator GoToTarget()
    {
        while (Vision.isTouchingLayer)
        {
            if (AttackRange.isTouchingLayer)
            {
                StartState(Attack());
            }
            else
            {
                SetDirection();
            }
            yield return null;
        }
        person.SetDirection(Vector2.zero);
        effects.Spawn("Miss");
        yield return new WaitForSeconds(missDelay);
    }
    private IEnumerator Attack()
    {
        while (AttackRange.isTouchingLayer)
        {
            person.SetDirection(Vector2.zero);
        person.Attack();
        yield return new WaitForSeconds(attackDelay);
        }
        if (!isDead)StartState(GoToTarget());
    }
    private void SetDirection()
    {
        var direction = target.transform.position - transform.position;
        direction.y = 0;
        person.SetDirection(direction.normalized);
    }
    public void OnDie()
    {
        collider2d.size = deadColliderSize;
        isDead = true;
        person.SetDirection(Vector2.zero);
        animator.SetBool("IsDead", true);
        if (currentCorotine!=null)
        {
            StopCoroutine(currentCorotine);
        }
    }
}
