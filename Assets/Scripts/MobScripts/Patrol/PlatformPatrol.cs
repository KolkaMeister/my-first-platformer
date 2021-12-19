using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPatrol : Patrol
{
    [SerializeField] RayCastLayerCheck bottomCheck;
    [SerializeField] RayCastLayerCheck straightCheck;
    private int direction=1;
    [SerializeField] private GamePerson person;
    public override IEnumerator DoPatrol()
    {
        while(enabled)
        {
            if (bottomCheck.isTouchingLayer&&!straightCheck.isTouchingLayer)
            {
                person.SetDirection(new Vector2 (direction,0));
            }else
            {
                direction = -direction;
                person.SetDirection(new Vector2(direction, 0));
            }
        yield return null;
        }
    }
}
