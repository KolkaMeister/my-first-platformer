using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularShootComponent : MonoBehaviour
{
    [SerializeField] public GameObject projectile;
    [SerializeField] public float delay;
    [SerializeField] public int count;
    [SerializeField] public float force;
    public void Shoot()
    {
        StartCoroutine(ShootingCoroutine());
    }
    public IEnumerator ShootingCoroutine()
    {
       var rotateAngle=2 * Mathf.PI / count;
        CalculateDirectionAndLaunch(0);
        yield return new WaitForSeconds(delay);
        CalculateDirectionAndLaunch(rotateAngle/3);
        yield return new WaitForSeconds(delay);
        CalculateDirectionAndLaunch(rotateAngle / 3*2);
    }
    public void CalculateDirectionAndLaunch(float rotationAngle)
    {
        var _step = 2 * Mathf.PI / count;
        for (int i = 0; i <count; i++)
        {
            var finalAngle = _step*i + rotationAngle;

            var dir = new Vector2(Mathf.Cos(finalAngle), Mathf.Sin(finalAngle));
            var proj = Instantiate(projectile,transform.position,Quaternion.identity);
            dir*=force;
            proj.GetComponent<Rigidbody2D>().AddForce(dir, ForceMode2D.Impulse);
            Destroy(proj, 15);
        }
    }
}
