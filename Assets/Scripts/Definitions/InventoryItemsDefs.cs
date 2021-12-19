using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[ CreateAssetMenu(menuName ="Defs/InventoryItems" ,fileName="InfentoryItems")]
public class InventoryItemsDefs : ScriptableObject
{
    [SerializeField] ItemDef[] itemsDefs;

    public ItemDef Get(string id)
    {
        foreach (var item in itemsDefs)
        {
            if (item.Id==id)
            {
                return item;
            }
        }
        return default;
    }
    public ItemDef[] GetTagget(params ItemsTag[] tags)
    {
        var TagItems = new List<ItemDef>();
        foreach (var def in itemsDefs)
        {
            var itemDef = DefsFacade.I.ItemDefs.Get(def.Id);
            var AllRequirementsMet = tags.All(x => itemDef.HasTag(x));
            if (AllRequirementsMet)
            {
                TagItems.Add(def);
            }
        }
        return TagItems.ToArray();
    }
}
[Serializable]
public struct ItemDef
{
    [SerializeField] public ItemsTag[] tags;
    [SerializeField]public string Id;
    [SerializeField]public int maxCount;
    [SerializeField] public Sprite Icon;
    public bool IsVoid => string.IsNullOrEmpty(Id);
    public bool HasTag(ItemsTag tag)
    {
        foreach (var _tag in tags)
        {
            if (_tag==tag)
            {
                return true;
            }
        }
        return false;
    }
}



