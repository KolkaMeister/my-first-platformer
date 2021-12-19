using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    [SerializeField] private float changePosX;
    [SerializeField] private float speed;
    private bool changed=false;
    private Vector3 startPos;
    private Vector3 secondPos;
    private Vector3 targetPos;
    void Start()
    {
        startPos = transform.position;
        secondPos = new Vector3(transform.position.x + changePosX, transform.position.y, transform.position.z);
        targetPos = startPos;
    }
    [ContextMenu("SetDir")]
    public void SetMoveDir()
    {
        if (!changed)
        {
            targetPos = secondPos;
            changed = true;
        }
        else
         if (changed)
        {
            targetPos = startPos;
            changed = !changed;
        }
    }
    private void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
    }

}
