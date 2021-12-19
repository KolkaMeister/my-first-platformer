using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    protected  Rigidbody2D  _rigidbody;
    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        direction = transform.lossyScale.x;
        var force = new Vector2(direction * speed, 0);
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }
    public virtual void Launch(Vector2 force)
    {
        if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(Mathf.Sign(force.x), Mathf.Sign(force.y), 1);
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}
