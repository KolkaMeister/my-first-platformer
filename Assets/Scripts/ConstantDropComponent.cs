using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConstantDropComponent : MonoBehaviour
{
    [SerializeField] ItemToDrop[] items;
    [SerializeField] int value;
    [SerializeField] public OnItemsCulculated ActWhithArray;

    public void CalculateDrop()
    {
        var index = 0;
        var itemsToDrop = new List<GameObject>();
        var sortedArray = items.OrderByDescending(itemToDrop => itemToDrop.value);
        foreach (var item in sortedArray)
        {
            while (value>=item.value)
            {
                value -= item.value;
                itemsToDrop.Add(item.item);
            }
            if (value <= 0) break;

        }
        ActWhithArray?.Invoke(itemsToDrop.ToArray());




    }



    [Serializable]
    public class ItemToDrop
    {
        [SerializeField] public GameObject item;
        [SerializeField] public int value;
    }

    [Serializable]
    public class OnItemsCulculated : UnityEvent<GameObject[]>
    { }

}
