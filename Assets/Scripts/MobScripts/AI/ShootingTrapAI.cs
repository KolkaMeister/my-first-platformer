using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrapAI : MonoBehaviour
{
    [SerializeField] private CheckCircleOverlap attackOverlap;
    [SerializeField] private ColliderCheck meleeCheck;
    [SerializeField] private ColliderCheck vision;
    [SerializeField] private Cooldown meeleAttackCooldown;
    [SerializeField] private Cooldown rangeAttackCooldown;
    [SerializeField] private SpawnListComponent spawners;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(vision.isTouchingLayer)
        {
            if (meleeCheck.isTouchingLayer)
            {
                if (meeleAttackCooldown.IsReady)
                {
                    OnMeleeAttack();
                    return;
                }
            }
            if (rangeAttackCooldown.IsReady)
            {

                OnRangeAttack();
            }

        }
    }
    private void OnMeleeAttack()
    {
        meeleAttackCooldown.Reset();
        animator.SetTrigger("MeleeAttack");
    }
    private void OnRangeAttack()
    {
        rangeAttackCooldown.Reset();
        animator.SetTrigger("RangeAttack");
    }
    public void MeleeAttack()
    {
        attackOverlap.CheckByTag();
    }
    public void RangeAttack()
    {
        spawners.Spawn("ThrowPearl");
    }














}
