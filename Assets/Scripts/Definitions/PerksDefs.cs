using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/PerksDefs", fileName = "PerksDefs")]
public class PerksDefs : ScriptableObject
{
    [SerializeField] private PerkDef[] defs;
    
    public PerkDef Get(string id)
    {
        foreach (var def in defs)
        {
            if (id==def.Id)
            {
                return def;
            }
        }
        return default;
    }
    public PerkDef[] GetAll => defs;

    public string[] GetAllID()
    {
         List<string> ids = new List<string>();
        foreach (var def in defs)
        {
            ids.Add(def.Id);
        }
        return ids.ToArray();
    }
}



[Serializable]
public struct PerkDef
{
    [SerializeField] private string id;
    [SerializeField] private float resetTime;
    [SerializeField] private Sprite icon;
    [SerializeField] private string info;
    [SerializeField] private ItemWithCount price;
    [SerializeField] private PerkType[] tags;
    public string Id => id;
    public float ResetTime=>resetTime;
    public Sprite Icon => icon;
    public string Info => info;
    public ItemWithCount Price => price;

    public PerkType[] Tags => tags;

    public bool HasTag(PerkType _tag)
    {
        foreach (var tag in tags)
        {
            if (tag == _tag) return true;
        }
        return false;
    }

}
[Serializable]
public struct ItemWithCount
{ 
    [SerializeField] private string id;
    [SerializeField] private int count;
    public string Id => id;
    public int Count => count;
}


