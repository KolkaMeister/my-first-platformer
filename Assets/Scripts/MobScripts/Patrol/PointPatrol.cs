using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPatrol : Patrol
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float treshold;
    private GamePerson person;
    private int destinationPointIndex;
    private void Awake()
    {
        person = GetComponent<GamePerson>();
    }
    public override IEnumerator DoPatrol()
    {
        while (enabled)
        {
            if (IsOnPoint())
            {
                destinationPointIndex = (int)Mathf.Repeat(destinationPointIndex + 1, points.Length);
            }
            var direction = points[destinationPointIndex].position - transform.position;
            direction.y = 0;
            person.SetDirection(direction.normalized);
            yield return null;
        }
    }
    private bool IsOnPoint()
    {
       return  (points[destinationPointIndex].position-transform.position).magnitude<treshold;
    }
}
