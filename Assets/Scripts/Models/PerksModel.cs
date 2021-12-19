using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PerksModel 
{
    private PlayerData data;

    private StringProperty selected = new StringProperty("");


    public event Action OnChanged;

    public string Selected => selected.Value;
    public PerksModel(PlayerData _data)
    {
        data = _data;
        selected.Value = data.Perks.Used;
    }
    public bool IsSelected(string _id)
    {
       return  selected.Value == _id;
    }
    public bool IsUsed(string id)
    {
        return data.Perks.Used == id;
    }
    public bool IsUnlocked(string id)
    {
        return data.Perks.IsUnlocked(id);
    }
    public bool IsEnough(string id)
    {
        var perk = DefsFacade.I.PerksDefs.Get(id);
        if (string.IsNullOrEmpty(perk.Id))
            return false;

       return data.Inventory.IsEnough(perk.Price);

    }

    public void Unlock(string id)
    {
        var perk = DefsFacade.I.PerksDefs.Get(id);
        if (IsEnough(id))
        {
            data.Perks.Add(id);
            OnChanged?.Invoke();
        }
    }

    public void Use(string id)
    {
        data.Perks.SetUsed(id);
        OnChanged?.Invoke();
    }
    
    public void Select(string id)
    {
        selected.Value = id;
        OnChanged?.Invoke();
    }
}
