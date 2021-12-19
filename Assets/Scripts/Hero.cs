using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Hero : GamePerson, ICanAddToInvenvoty
{
    [Header("checkers")]
    [SerializeField] protected CheckCircleOverlap interactOverlap;
    [SerializeField] private ColliderCheck wallCheck;
    [Header("other")]
    [SerializeField] public GameObject playerCandle;
    [SerializeField] private float interactCircleRadius;
    [SerializeField] private Cooldown jumpCooldown;
    [SerializeField] private bool allowDoubleJump;
    [SerializeField] private ParticleSystem coinsBurst;
    [SerializeField] private float fallDustDelta;
    [SerializeField] private RuntimeAnimatorController armed;
    [SerializeField] private RuntimeAnimatorController nonArmed;
    [SerializeField] private AudioPlayComponent audioPlayer;
    [Header("ThrowParams")]
    [SerializeField] private SpawnComponent projectileSpawner;
    [SerializeField] private Cooldown throwCooldown;
    [SerializeField] private Cooldown multiThrowCooldown;
    [SerializeField] private bool multiThrow;
    [SerializeField] private float multiThrowDelay = 0.1f;
    [Header("Perks")]
    [SerializeField] private float teleportRayDistance;
    [SerializeField] private float shieldTime;
    [SerializeField] private GameObject shield;
    [SerializeField] private LayerMask teleportColliderLayers;
    [Header("HeroCharacteristics")]
    private int maxHealth;
    [Space]
    [SerializeField] private ShakeCameraComponent shakeCamera;
    [Header("Abilities")]
    public AbilityManager abilityManager=new AbilityManager();

    public SpriteRenderer _renderer;
    public GameSession gameSession;
    private float fallStartTime;
    private bool fallStarted;
    public bool hookPressed;
    private bool isOnWall;
    private Collider2D[] interactionResult = new Collider2D[1];
    private HealthComponent health;
    private FixedJoint2D joint;
    private int numCoins => gameSession.data.Inventory.Count("Coin");

    private const string SwordID = "Sword";
    private int numSwords => gameSession.data.Inventory.Count("Sword");
    private int numPotions => gameSession.data.Inventory.Count("HealPotions");

    private string SelectedItemId => gameSession.quickInventory.SelectedItem != null ? gameSession.quickInventory.SelectedItem.Id : string.Empty;

    private bool AllowToThrow
    {
        get
        { if (SelectedItemId == string.Empty) return false;
            if (SelectedItemId == SwordID)
                return numSwords > 1;
            var itemDef = DefsFacade.I.ItemDefs.Get(SelectedItemId);
            if (itemDef.HasTag(ItemsTag.Throwable))
                return true;
            return false;
            
        }
    }

    static readonly int VerticalVelocityKey = Animator.StringToHash("Vertical-Velocity");
    static readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    static readonly int IsOnGroundKey = Animator.StringToHash("IsOnGround");
    static readonly int IsFallingKey = Animator.StringToHash("IsFalling");
    static readonly int ThrowKey = Animator.StringToHash("Throw");
    protected override void Awake()
    {
        base.Awake();
        InitAbilities();
    }
    private void Start()
    {
        joint = GetComponent<FixedJoint2D>();
        _renderer = GetComponent<SpriteRenderer>();
        gameSession = FindObjectOfType<GameSession>();
        CheckArm();
        health = GetComponent<HealthComponent>();
        maxHealth = gameSession.data.Stats.GetStats(Characteristics.Hp);
        health.maxHealth = maxHealth;
        health.SetHealth(gameSession.data.Hp.Value);
        defaultSpeed = gameSession.statsModel.GetStatValue(Characteristics.Speed);
        speed = defaultSpeed;
        gameSession.data.Inventory.onChange += CheckArm;
        gameSession.statsModel.onStatsChanged += OnStatsChanged;
        shakeCamera = FindObjectOfType<ShakeCameraComponent>();
    }
    private void InitAbilities()
    {
        abilityManager.abilities = new List<AbilityData>() {
        new AbilityData(SuperThrow, DefsFacade.I.PerksDefs.Get("super_throw").ResetTime, "super_throw"),
        new AbilityData(DivineShield, DefsFacade.I.PerksDefs.Get("divine_protection").ResetTime, "divine_protection"),
        new AbilityData(Teleport, DefsFacade.I.PerksDefs.Get("teleport").ResetTime, "teleport"),
    };
    }
    public void OnStatsChanged(Characteristics id)
    {
        switch (id)
        {
            case Characteristics.Hp:

                maxHealth = gameSession.statsModel.GetStatValue(id);
                health.maxHealth = maxHealth;
                break;
            case Characteristics.Damage:
                break;
            case Characteristics.Speed:
                defaultSpeed = gameSession.statsModel.GetStatValue(id);
                speed = defaultSpeed;
                break;
            default:
                break;
        }
    }
    private void OnDestroy()
    {
        if (gameSession == null) return;
        if (gameSession.data != null && gameSession.statsModel != null)
        {
            gameSession.data.Inventory.onChange -= CheckArm;
        }
        if (gameSession.statsModel != null)
        {
            gameSession.statsModel.onStatsChanged -= OnStatsChanged;
        }


    }
    public void CheckArm(string id, int count)
    {
        if (id == "Sword")
        {
            CheckArm();
        }
    }
    public void SpeedBuf(float value, float time)
    {
        StartCoroutine(BuffSpedd(value, time));
    }
    private IEnumerator BuffSpedd(float value, float time)
    {
        var defaultSpeed = speed;
        speed *= value;
        yield return new WaitForSeconds(time);
        speed = defaultSpeed;
    }
    public void SaveHealth(int health)
    {
        gameSession.data.Hp.Value = health;
    }

    protected override void FixedUpdate()
    {
        IsOnWall();
        if (isOnWall)
        {
            var velocityX = 0;
            var velocityY = 0;
            rigidbody.velocity = new Vector2(velocityX, velocityY);
            allowDoubleJump = true;
        }
        else
        {
            var velocityX = direction.x * speed;
            var velocityY = CalculateVelocityY();
            UpdateSpriteFlip();
            rigidbody.velocity = new Vector2(velocityX, velocityY);
        }

        animator.SetFloat(VerticalVelocityKey, rigidbody.velocity.y);
        animator.SetBool(IsRunningKey, direction.x != 0);
        animator.SetBool(IsOnGroundKey, groundCheck.isTouchingLayer);
        animator.SetBool(IsFallingKey, rigidbody.velocity.y <= -0.01f);
        FallTime();
    }
    protected override float CalculateVelocityY()
    {
        var velocityY = rigidbody.velocity.y;
        var isJumping = direction.y > 0;
        if (groundCheck.isTouchingLayer) allowDoubleJump = true;
        if (isJumping&& jumpCooldown.IsReady)
        {
            velocityY = CalculateJumpVelocityY(velocityY);
        }
        return velocityY;
    }
    public void IsOnWall()
    {
        if (wallCheck.isTouchingLayer && hookPressed)
        {
            isOnWall = true;
        } else
        {
            isOnWall = false;
        }
    }
    protected override float CalculateJumpVelocityY(float velocityY)
    {
        var isFalling = rigidbody.velocity.y <= 0.01f;
        if (!isFalling) return velocityY;
        if (groundCheck.isTouchingLayer)
        {
            jumpCooldown.Reset();
            SpawnJumpDust();
            velocityY += jumpForce;
            audioPlayer.Play("Jump");
        }
        else if ((allowDoubleJump && gameSession.data.Perks.DoubleJump) || wallCheck.isTouchingLayer)
        {
            SpawnJumpDust();
            jumpCooldown.Reset();
            velocityY = jumpForce;
            audioPlayer.Play("Jump");
            allowDoubleJump = false;
        }
        return velocityY;
    }
    public void KeyWord()
    {
        Debug.Log("rom!");
    }
    //private void OnDrawGizmos()
    //{
    //    Handles.color = groundCheck.isTouchingLayer ? HandlesUtils.TransparentGreen: HandlesUtils.TransparentRed;
    //    Handles.DrawSolidDisc(transform.position + groundPosDelta,Vector3.forward ,groundCheckRadius);
    //}
    public override void OnTakeDamage()
    {
        base.OnTakeDamage();
        audioPlayer.Play("Hit");
        if (numCoins > 0) PlayCoinsBurstParticle();
        shakeCamera.Shake();
    }
    public override void OnHeal()
    {
        Debug.Log("I feel Great!!");
    }
    private void PlayCoinsBurstParticle()
    {
        var coinsToDrop = Mathf.Min(numCoins, 5);
        gameSession.data.Inventory.Remove("Coins", coinsToDrop);

        var burst = coinsBurst.emission.GetBurst(0);
        burst.count = coinsToDrop;
        coinsBurst.gameObject.SetActive(true);

        coinsBurst.Play();
    }
    public void Interact()
    {
        interactOverlap.CheckByLayer();
    }

    private void FallTime()
    {
        if (!groundCheck.isTouchingLayer && !fallStarted)
        {
            fallStarted = true;
            fallStartTime = Time.time;
        }
        if (groundCheck.isTouchingLayer && fallStarted)
        {
            fallStarted = false;
            var deltaTime = Time.time - fallStartTime;
            if (deltaTime >= fallDustDelta)
            {
                SpawnFallDust();
            }
        }

    }
    private void SpawnFallDust()
    {
        Effects.Spawn("SlamDown");
    }
    public override void Attack()
    {
        if (numSwords <= 0) return;
        animator.SetTrigger("Attack");
    }
    public override void IfAttack()
    {
        base.IfAttack();
        audioPlayer.Play("MeleeAttack");

    }
    public void AddToInventory(string id, IntProperty count)
    {
        gameSession.data.Inventory.Add(id, count);
    }
    public void Arm()
    {
        CheckArm();
    }
    private void CheckArm()
    {
        animator.runtimeAnimatorController = numSwords > 0 ? armed : nonArmed;
    }

    public void Heal()
    {
        if (SelectedItemId == string.Empty) return;
        var itemDef = DefsFacade.I.ItemDefs.Get(SelectedItemId);
        var has = itemDef.HasTag(ItemsTag.Healable);
        if (!has) return;
        var value = DefsFacade.I.HealItemsDef.Get(SelectedItemId).HealValue;
        health.ApplyHeal(value);
        gameSession.data.Inventory.Remove(SelectedItemId, 1);

    }
    public void Throw()
    {
        var itemDef = DefsFacade.I.ThrowableItemDefs.Get(SelectedItemId);
        projectileSpawner.SetPrefab(itemDef.Prefab);
        if (multiThrow&& abilityManager.Get("super_throw").cooldown.IsReady)
        {
            abilityManager.Get("super_throw").Use();
        }
        else
        {
            multiThrow = false;
            DoThrow();
        }
    }
    public bool SuperThrow()
    {
        if (!AllowToThrow|| !abilityManager.Get("super_throw").cooldown.IsReady) return false;
        var numToThrow = gameSession.data.Inventory.Count(SelectedItemId);
        if (SelectedItemId == SwordID) numToThrow -= 1;
        var amount = Mathf.Min(3, numToThrow);
        StartCoroutine(MultiThrow(amount));
        multiThrow = false;
        return true;
    }
    public IEnumerator MultiThrow(int count)
    {
        for (int i = 0; i < count; i++)
        {
            DoThrow();
            yield return new WaitForSeconds(multiThrowDelay);
        }
    }
    public void DoThrow()
    {
        audioPlayer.Play("RangeAttack");
        gameSession.data.Inventory.Remove(SelectedItemId,1);
        projectileSpawner.Spawn();
    }
    public void ThrowStarted()
    {
        multiThrowCooldown.Reset();
    }
    public void ThrowComplete()
    {

        if (!throwCooldown.IsReady || !AllowToThrow) return;
        if (multiThrowCooldown.IsReady&& gameSession.data.Perks.SuperThrow) multiThrow = true;
        throwCooldown.Reset();
        animator.SetTrigger(ThrowKey);

    }
    public void ChangeSelection()
    {
        gameSession.quickInventory.NextItem();
    }
    public void OnDie()
    {
        gameSession.ReloadLevel();
    }
    public bool DivineShield()
    {
        if (!gameSession.data.Perks.DivineShield || !abilityManager.Get("divine_protection").cooldown.IsReady) return false;
            StartCoroutine(Invulnerability());
        return true;
    }
    private IEnumerator Invulnerability()
    {
        health.locker.Retain(this);
        shield.SetActive(true);
        yield return new WaitForSeconds(shieldTime);
        health.locker.Dispose(this);
        shield.SetActive(false);
    }
    public bool Teleport()
    {
        if (gameSession.data.Perks.Used != "teleport"|| !abilityManager.Get("teleport").cooldown.IsReady) return false;

        var direction = new Vector3(1 * transform.lossyScale.x, 0,0);
        var ray = new Ray(transform.position, direction);
        var hit=Physics2D.Raycast(transform.position, direction, teleportRayDistance+0.3f, teleportColliderLayers);
        if (hit.collider==null)
        {
            direction.x *= teleportRayDistance;
            transform.position += direction;
        }
        else
        {
            direction.x *= (hit.distance-0.3f);
            transform.position += direction;
        }
        return true;
    }
    public void FillFuel(float _value)
    {
        gameSession.data.LightFuel.Value = Mathf.Min(100f, gameSession.data.LightFuel.Value + _value);
    }
    public void UseCandle()
    {
        if (gameSession.data.LightFuel.Value>0)
        {
            playerCandle.SetActive(!playerCandle.active);
            gameSession.data.IsShining.Value = playerCandle.active;
        }
        
    }
    public void ConnectJoint (Rigidbody2D _rb) 
    {
        joint.connectedBody = _rb;
    }
    public void BreakJoint()
    {
        joint.connectedBody = rigidbody;
    }

}
