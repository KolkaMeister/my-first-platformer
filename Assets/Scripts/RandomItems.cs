using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomItems : MonoBehaviour
{
    [SerializeField] private int count;
    [SerializeField] private ItemDrop[] items;
    [SerializeField] public OnItemsCulculated ActWhithArray;
    [SerializeField] private bool playOnEnable;

    [ContextMenu("calculate")]
    public void CalculateDrop()
    {
        var itemsToDrop = new GameObject[count];
        var totalChance = items.Sum(itemDrop=>itemDrop.chance);
        var sortedItems = items.OrderBy(itemDrop => itemDrop.chance);
        for (int i = 0; i < count; i++)
        {
            var random = UnityEngine.Random.value * totalChance;
            foreach (var item in sortedItems)
            {
                random-= item.chance;
                if (random <= 0)
                {
                    itemsToDrop[i] = item.Item;
                    break;
                }
            }
        }
        ActWhithArray?.Invoke(itemsToDrop);
    }
    private void OnEnable()
    {
        if (playOnEnable)
        {
            CalculateDrop();
        }
    }


    [Serializable]
    public class ItemDrop
    {
        public GameObject Item;
        public int chance;
    }
    [Serializable]
    public class OnItemsCulculated : UnityEvent<GameObject[]>
    { }

}
