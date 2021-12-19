using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOfDeathController : MonoBehaviour
{
    [SerializeField] public HealthComponent health;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private BallOfDeath ball;
    [SerializeField] public int damage;

    private int spawnPointIdnex;

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        Debug.Log("Spawn");
        if (health.Health <= 0) return;
         var instance=Instantiate(ball, spawnPoints[spawnPointIdnex].position, Quaternion.identity);
        instance.controller = this;
        spawnPointIdnex = (int)Mathf.Repeat(spawnPointIdnex+1, spawnPoints.Length) ;

    }
}
