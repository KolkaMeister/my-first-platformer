using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkStarAI : MonoBehaviour
{
    [Header("ProjectileSpawnKonfig")]
    [SerializeField] GameObject projectile;
    [SerializeField] float rotateAngle;
    [SerializeField] float randomizeAngle;
    [SerializeField] float throwForce;
    [Space]
    [SerializeField] private AudioPlayComponent audioPlayer;
    [SerializeField] private LayerCheck wallCheckRay;
    [SerializeField] private LayerCheck groundCheckRay;
    [SerializeField] private LayerCheck vision;
    [SerializeField] private Transform[] points;
    [SerializeField] private float changePointDistance;
    [SerializeField] private float patrolDelay;
    [SerializeField] private float CatchDistance;
    [SerializeField] private float runDistance;
    [SerializeField] private Cooldown attackCooldown;
    [SerializeField] private CapsuleCollider2D collider2d;
    [SerializeField] Vector2 deadColliderSize;
    private GamePerson person;
    private Animator animator;
    private GameObject target;
    private int patrolPointIndex;
    private Coroutine currentCoroutine;
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private bool isDead;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        person = GetComponent<GamePerson>();
    }
    private void Start()
    {
        StartState(PointPatrol());
    }

    private IEnumerator PointPatrol()
    {
        while (enabled)
        {
            if (IsOnPoint())
            {
                patrolPointIndex = (int)Mathf.Repeat(patrolPointIndex + 1, points.Length);
                person.SetDirection(Vector2.zero);
                yield return new WaitForSeconds(patrolDelay);
            }
            yield return null;
            var dir =  points[patrolPointIndex].position - transform.position;
            dir.y = 0;
            person.SetDirection(dir.normalized);
            //if (wallCheckRay.isTouchingLayer||!groundCheckRay.isTouchingLayer)
            //    {
            //       StartState(PlatformPatrol());
            //       yield return new WaitForSeconds(0.5f);
            //    }
        }
    }
    private IEnumerator PlatformPatrol()
    {
        var directionX =1;
       while(enabled)
        {
            if (wallCheckRay.isTouchingLayer || !groundCheckRay.isTouchingLayer)
            {
                person.SetDirection(Vector2.zero);
                yield return new WaitForSeconds(patrolDelay);
                directionX = -directionX;
            }
            Debug.Log(directionX);
            yield return null;
            var direction = new Vector2(directionX, 0);
            person.SetDirection(direction);
        }

    }
    public void Agro(GameObject _target)
    {
        if (isDead) return;
        target = _target;
        StartState(Fight());

    }
    private IEnumerator Fight()
    {
        while(vision.isTouchingLayer)
        {
            var vector = target.transform.position - transform.position;
            var distance = vector.magnitude;
            if (distance > CatchDistance)
            {
                var direction = new Vector2(vector.x, 0);
                person.SetDirection(direction.normalized);
            }
            else if (distance<runDistance)
            {
                var direction = new Vector2(-vector.x, 0);
                person.SetDirection(direction.normalized);
            }
            else
            {
                var direction = new Vector2(-vector.x, 0);
                person.SetDirection(direction.normalized);
                person.SetDirection(Vector2.zero);
            }
            if (attackCooldown.IsReady)
            {
                attackCooldown.Reset();
                DoAttack();
            }
            yield return null;

        }
        if (!isDead)
        StartState(Miss());
    }

    private IEnumerator Miss()
    {
        person.SetDirection(Vector2.zero);
        yield return new WaitForSeconds(1);
        StartState(PointPatrol());
    }

    public void OnDie()
    {
        if (currentCoroutine!=null)
        {
            StopCoroutine(currentCoroutine);
        }
        person.SetDirection(Vector2.zero);
        isDead = true;
        animator.SetBool(IsDead,isDead);
        collider2d.size = deadColliderSize;
        FindObjectOfType<GameSession>().data.EventsData.AddCreature("PinkStar");
    }

    private void DoAttack()
    {
        animator.SetTrigger(Attack);
    }

    public void SpawnProjectile()
    {
       audioPlayer.Play("Attack");
        ThrowProjectile();
    }

    public void StartState(IEnumerator coroutine)
    {
        if (currentCoroutine!=null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(coroutine);
    }
    private bool IsOnPoint()
    {
        return (transform.position - points[patrolPointIndex].position).magnitude <= changePointDistance;
    }
    [ContextMenu("throwProjectile")]
    private void ThrowProjectile()
    {
        var instance = Instantiate(projectile, transform.position, Quaternion.identity);
        
       var randomAngle = Random.Range(0, randomizeAngle);
       var forceVector = CalculateForce(randomAngle);
        var projectileComponent =  instance.GetComponent<AimTargetProjectile>();
       projectileComponent.rigidbody2d.AddForce(forceVector, ForceMode2D.Impulse);
        projectileComponent.SetAndLaunch(target);
    }
    private Vector2 CalculateForce(float randomAngle)
    {
        var angle = (180 - randomizeAngle - rotateAngle) / 2 + randomAngle;
        var angleInRad = angle * Mathf.PI / 180;
        var x = Mathf.Cos(angleInRad);
        var y = Mathf.Sin(angleInRad);
        return new Vector2(x* throwForce, y* throwForce);

    }
}
