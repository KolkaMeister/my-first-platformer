using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Defs/HealItemsDef" , fileName = "HealItemsDef")]
public class HealItemsDef : ScriptableObject
{
    [SerializeField] HealItem[] items;

    public HealItem Get(string id)
    {
        foreach (var item in items)
        {
            if (item.Id == id)
            {
                return item;
            }
        }
        return default;
    }
}
[Serializable]
public struct HealItem
{
    [SerializeField] private string id;
    [SerializeField] private int healValue;

    public string Id => id;
    public int HealValue => healValue;
}