using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTrapAI : MonoBehaviour
{
    [SerializeField] private ColliderCheck vision;
    [SerializeField] private Cooldown attackCooldown;
    [SerializeField] private SpawnListComponent spawners;
    [SerializeField] private AudioPlayComponent audioPlayer;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (vision.isTouchingLayer)
        {
            if (attackCooldown.IsReady)
            {
                Attack();
            }
        }
    }
    private void Attack()
    {
        attackCooldown.Reset();
        animator.SetTrigger("Fire");
       
    }
    public void OnAttack()
    {
        spawners.Spawn("ThrowBall");
        spawners.Spawn("FireEffect");
        audioPlayer.Play("Fire");
    }
}
