using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTargetProjectile : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private float force;
    [SerializeField] private GameObject target;
    public Rigidbody2D rigidbody2d;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }


    [ContextMenu("Launch")]
    public void Launch()
    {
        StartCoroutine(Attack());
    }
    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(delay);
        if (target == null)
        {
            Destroy(gameObject);
            yield return null;
            yield return null;
        }
        var direction = (target.transform.position- transform.position).normalized;
        direction = new Vector2(direction.x * force, direction.y * force);
        rigidbody2d.velocity = Vector2.zero;
        rigidbody2d.AddForce(direction, ForceMode2D.Impulse);
    }
    public void SetTarget(GameObject _target)
    {
        target = _target;
    }
    public void SetAndLaunch(GameObject _target)
    {
        SetTarget(_target);
        Launch();
    }
    private void OnDestroy()
    {
    }
}
