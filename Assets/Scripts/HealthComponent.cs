using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    [SerializeField] private int health;
    [SerializeField] public UnityEvent onDamage;
    [SerializeField] public UnityEvent onHeal;
    [SerializeField] public UnityEvent onDie;
    [SerializeField] public HealthChangeEvent onChange;
    
    public int oldValue;
    public Action<int> onHpChanged;

    public Locker locker = new Locker();
    private bool Protection=> locker.Lockers.Count>0;

    public bool isDead;
    public int Health => health;
    private void Start()
    {
        oldValue = health;
    }
    public void ApplyDamage(int damageValue)
    {
        if (Protection) return;
        oldValue = health;
        health -= Mathf.Min(damageValue, health);
        onDamage?.Invoke();
        onChange?.Invoke(health);
        onHpChanged?.Invoke(Health);
        
        if (health<=0)
        {
            if (!isDead)
            {
                onDie?.Invoke();
                isDead = true;
            }
        }
    }
    public void ApplyHeal(int healValue)
    {
        oldValue = health;
        health = Mathf.Clamp(health + healValue, 0, maxHealth); ;
        onHeal?.Invoke();
        onChange?.Invoke(health);
        onHpChanged?.Invoke(Health);
        if (health <= 0)
        {
            if (!isDead)
            {
                onDie?.Invoke();
                isDead = true;
            }
        }
    }
    public void SetHealth(int hp)
    {
        health = hp;
        oldValue = hp;
    }
    [Serializable]
    public class HealthChangeEvent : UnityEvent<int>
    {
    }


}
