using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/ThrowableItemDefs", fileName = "ThrowableItemDefs")]
public class ThrowableItemDefs : ScriptableObject
{
    [SerializeField] private ThrowableItem[] items;
    public ThrowableItem Get(string id)
    {
        foreach (var item in items)
        {
            if (item.Id==id)
            {
                return item;
            }
        }
        return default;
    }
}

[Serializable]
public struct ThrowableItem
{
    [SerializeField] private string id;
    [SerializeField] private GameObject prefab;

    public string Id => id;
    public GameObject Prefab => prefab;

}
