using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOfDeathController : MonoBehaviour
{
    [SerializeField] public HealthComponent health;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private BallOfDeath ball;
    [SerializeField] public int damage;

    private List<BallOfDeath> balls = new List<BallOfDeath>();
    private int spawnPointIdnex;

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        if (balls.Count>0) return;
        if (health.Health <= 0) return;
        var instance=Instantiate(ball, spawnPoints[spawnPointIdnex].position, Quaternion.identity);
        instance.controller = this;
        instance.balls = balls;
        spawnPointIdnex = (int)Mathf.Repeat(spawnPointIdnex+1, spawnPoints.Length) ;

    }
    public void DealDamage()
    {
        health.ApplyDamage(damage);
    }
}
