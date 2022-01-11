using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [Header("Delays")]
    [SerializeField] private float spawnDelay;
    [SerializeField] private float launchDelay;
    [SerializeField] private float spawnAndLaunchDelay;
    [Space]
    [SerializeField] private float force;
    [SerializeField] private int count;
    [SerializeField] private StarProjectile projectile;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] public GameObject target;

    private Coroutine launchCoroutine;
    [ContextMenu("Launch")]
    public void StartLaunching()
    {
         StartCoroutine(Launch());
    }
    private IEnumerator Launch()
    {
        var projectiles = new List<StarProjectile>();
        for (int i = 0; i < count&& i < spawnPoints.Length; ++i)
        {
            projectiles.Add(Instantiate(projectile, spawnPoints[i].position,Quaternion.identity));
            yield return new WaitForSeconds(spawnDelay);
        }
        yield return new WaitForSeconds(spawnAndLaunchDelay);
        for (int i = 0; i < projectiles.Count; i++)
        {
            var projectile = projectiles[i];
            if (projectile!=null)
            {
            var _force = (target.transform.position - projectile.transform.position).normalized* force;
            projectile.Launch(_force);
            }
            yield return new WaitForSeconds(launchDelay);
        }    
    }
    public void StopLaunch()
    {
        if (launchCoroutine!=null) StopCoroutine(launchCoroutine);
        DestroyAllProjectile();
    }
    private void DestroyAllProjectile()
    {
       var stars = FindObjectsOfType<StarProjectile>();
        foreach (var star in stars)
        {
            star.DestroyAnimation();
        }
    }
}
