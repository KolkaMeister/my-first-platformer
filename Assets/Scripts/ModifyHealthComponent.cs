using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyHealthComponent : MonoBehaviour
{
    [SerializeField] private int value;
    public void Damage(GameObject target)
    {
        var health = target.GetComponent<HealthComponent>();
        if (health!=null)
        {

        health.ApplyDamage(value);
        }
    }
    public void Heal(GameObject target)
    {
        var health = target.GetComponent<HealthComponent>();
        if (health != null)
        {

            health.ApplyHeal(value);
        }
    }
}
