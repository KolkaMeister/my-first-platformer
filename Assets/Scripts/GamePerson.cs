using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GamePerson : MonoBehaviour
{
    [SerializeField] protected float defaultSpeed= 3f;
    [SerializeField] protected float speed = 3f;
    [SerializeField] protected float defaultJumpForce = 3f;
    [SerializeField] protected float jumpForce = 3f;
    [SerializeField] protected float damageJumpForce;
    [SerializeField] protected int damage = 3;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected Vector3 groundPosDelta;
    [SerializeField] protected float groundCheckRadius;
    [SerializeField] public SpawnListComponent Effects;
    [SerializeField] protected CheckCircleOverlap attackOvelap;
    [SerializeField] protected ColliderCheck groundCheck;

    protected Vector2 direction;
    protected Rigidbody2D rigidbody;
    protected Animator animator;
    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        var health = GetComponent<HealthComponent>();
    }
    public void SetDirection(Vector2 newDir)
    {
        direction = newDir;
    }
    protected virtual float CalculateVelocityY()
    {
        var velocityY = rigidbody.velocity.y;
        var isJumping = direction.y > 0;
        if (isJumping)
        {
            velocityY = CalculateJumpVelocityY(velocityY);
        }
        return velocityY;
    }
    protected virtual float CalculateJumpVelocityY(float velocityY)
    {
        var isFalling = rigidbody.velocity.y <= 0.03f;
        if (!isFalling) return velocityY;
        if (groundCheck.isTouchingLayer)
        {
            SpawnJumpDust();
            velocityY += jumpForce;
        }
        return velocityY;
    }
    protected void UpdateSpriteFlip()
    {
        if (direction.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    protected virtual void FixedUpdate()
    {
        var velocityX = direction.x * speed;
        var velocityY = CalculateVelocityY();
        rigidbody.velocity = new Vector2(velocityX, velocityY);

        animator.SetFloat("Vertical-Velocity", rigidbody.velocity.y);
        animator.SetBool("IsRunning", direction.x != 0);
        animator.SetBool("IsOnGround", groundCheck.isTouchingLayer);
        animator.SetBool("IsFalling", rigidbody.velocity.y <= -0.01f);
        UpdateSpriteFlip();
    }
    public virtual void OnHeal()
    {
        Debug.Log("I feel Great!!");
    }
    public virtual void OnTakeDamage()
    {
        animator.SetTrigger("Hit");
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, damageJumpForce);
    }
    public void SpawnFootDust()
    {
        Effects.Spawn("Run");
    }
    public void SpawnJumpDust()
    {
        Effects.Spawn("Jump");
    }
    public void SpawnAttackEffect()
    {
        Effects.Spawn("Attack");
    }
    public virtual void Attack()
    {
        animator.SetTrigger("Attack");
    }
    public virtual void IfAttack()
    {
        attackOvelap.CheckByTag();
    }
    protected virtual bool IsGrounded()
    {
        var hit = Physics2D.CircleCast(transform.position + groundPosDelta, groundCheckRadius, Vector2.down, 0, groundLayer);
        return hit.collider != null;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = IsGrounded() ? HandlesUtils.TransparentGreen : HandlesUtils.TransparentRed;
        Handles.DrawSolidDisc(transform.position + groundPosDelta, Vector3.forward, groundCheckRadius);
    }
#endif
}
