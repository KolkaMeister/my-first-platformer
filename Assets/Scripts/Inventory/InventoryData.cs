using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryData 
{
    [SerializeField] private List<InventoryItemData> inventory;
    public delegate void OnInventoryChange(string id, int value);
    public OnInventoryChange onChange;
    public InventoryItemData[] InventoryItems => inventory.ToArray();
    public void Add(string id, IntProperty count)
    {

        if (count.Value <= 0) return;

        var itemDef = DefsFacade.I.ItemDefs.Get(id);
        if (itemDef.IsVoid) return;
        var item = FindItemInInventory(id);
        if (item == null)
        {
            item = new InventoryItemData(id, itemDef.maxCount);
            inventory.Add(item);
        }
        var freeSpace = item.maxCount - item.count;
        var countToAdd = Mathf.Min(freeSpace, count.Value);
        item.count += countToAdd;
        count.Value -= countToAdd;
        onChange?.Invoke(id, Count(id));
    }
    public void Add(string id, int count)
    {

        if (count <= 0) return;

        var itemDef = DefsFacade.I.ItemDefs.Get(id);
        if (itemDef.IsVoid) return;
        var item = FindItemInInventory(id);
        if (item == null)
        {
            item = new InventoryItemData(id, itemDef.maxCount);
            inventory.Add(item);
        }
        var freeSpace = item.maxCount - item.count;
        var countToAdd = Mathf.Min(freeSpace, count);
        item.count += countToAdd;
        onChange?.Invoke(id, Count(id));
    }
    public void Remove(string id, int count)
    {

        var item = FindItemInInventory(id);
        if (item == null) return;
        item.count -= count;
        if (item.count <= 0) inventory.Remove(item);
        onChange?.Invoke(id, Count(id));
    }
    public InventoryItemData[] GetItems(params ItemsTag[] tags)
    {
        var TagItems = new List<InventoryItemData>();
        foreach (var item in inventory)
        {
            var itemDef = DefsFacade.I.ItemDefs.Get(item.Id);
            var AllRequirementsMet = tags.All(x => itemDef.HasTag(x));
            if (AllRequirementsMet)
            {
                TagItems.Add(item);
            }
        }
        return TagItems.ToArray();
    }
    public int Count(string id)
    {
        var count = 0;
        foreach (var item in inventory)
        {
            if (id == item.Id)
            {
                count += item.count;
            }
        }
        return count;
    }
    public bool IsEnough(params ItemWithCount[] price)
    {
        var priceList = new Dictionary<string, int>();

        foreach (var item in price)
        {
            if (priceList.ContainsKey(item.Id))
                priceList[item.Id] += item.Count;
            else
                priceList.Add(item.Id, item.Count);
        }
        foreach (var item in priceList)
        {
            if (item.Value>Count(item.Key))
            {
                return false;
            }
        }

        return true;

    }

    private InventoryItemData FindItemInInventory(string id)
    {
        foreach (var item in inventory)
        {
            if (id == item.Id)
            {

                return item;

            }
        }

        return null;
    }


}


[Serializable]
public class InventoryItemData
{
    public string Id;
    public int count;
    public int maxCount;
    public InventoryItemData(string id,int MaxCount)
    {
       Id = id;
        maxCount = MaxCount;
    }
    public bool FullStacked => count >= maxCount;
}
