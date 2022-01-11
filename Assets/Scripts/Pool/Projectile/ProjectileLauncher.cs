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
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] public GameObject target;
    private List<Projectile> projectiles = new List<Projectile>();

    private Coroutine launchCoroutine;
    [ContextMenu("Launch")]
    public void StartLaunching()
    {
        launchCoroutine = StartCoroutine(Launch());
    }
    private IEnumerator Launch()
    {
        List<Projectile> projectiles = new List<Projectile>();
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
        StopCoroutine(launchCoroutine);
    }
}
