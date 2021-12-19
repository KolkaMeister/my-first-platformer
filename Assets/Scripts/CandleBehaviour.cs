using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleBehaviour : MonoBehaviour
{

    public void FillFueld(GameObject go)
    {
        var hero = go.GetComponent<Hero>();

        if (hero!=null)
        {
            hero.FillFuel(100f);
        }
    }


}
