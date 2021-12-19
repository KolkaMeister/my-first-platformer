using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemHead : MonoBehaviour
{
    [SerializeField] private SpawnComponent projectileSpawner;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioSource source;
    private Animator animator;
    static readonly int Attack = Animator.StringToHash("Attack");

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void DoAttack()
    {
        animator.SetTrigger(Attack);
    }
    public void Shoot()
    {
        projectileSpawner.Spawn();
        if (source != null)
            source.PlayOneShot(shootSound);
    }
}
