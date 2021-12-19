using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalMovement : MonoBehaviour
{

    [SerializeField] float amplitude=1f;
    [SerializeField] float frequency = 1f;
    [SerializeField] bool randomize;
     Rigidbody2D rigidbody;
    private float seed;
    private float originalY;
    private float time;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        originalY = rigidbody.position.y;

    }
    private void Update()
    {
        var pos = rigidbody.position;
        pos.y = originalY + Mathf.Sin(time*frequency)*amplitude;
        rigidbody.MovePosition(pos);
        time = time+ Time.fixedDeltaTime;
    }
}

