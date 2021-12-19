using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePearl : Projectile
{
    [SerializeField] float amplitude = 1f;
    [SerializeField] float frequency = 1f;
    private float originalY;
    private float time;
    protected override void Start()
    {
        base.Start();
        originalY = GetComponent<Rigidbody2D>().position.y;
    }
    private void Update()
    {
        var position = transform.position;
        position.y = originalY + Mathf.Sin(time * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, position.y, transform.position.z);
        time += Time.fixedDeltaTime;
    }
}
