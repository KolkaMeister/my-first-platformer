using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsModel
{
    private PlayerData data;

    private CharacteristicsProperty selectedStat = new CharacteristicsProperty(Characteristics.Hp);

    public CharacteristicsProperty SelectedStat => selectedStat;

    public delegate void OnStatsChanged(Characteristics _id);

    public OnStatsChanged onStatsChanged;

    public StatsModel(PlayerData _data)
    {
        data = _data;
    }

    public int GetStatValue(Characteristics _id) =>data.Stats.GetStats(_id);


    public int GetStatLevel(Characteristics id)
    {
        return data.Stats.GetStatLevel(id);
    }
    public void UpgradeStat(Characteristics id)
    {
        var def =  DefsFacade.I.HeroStatsDef.Get(id);
        var currentLevel =data.Stats.GetStatLevel(id);
        if (currentLevel + 1 > def.MaxLevel) return;
        var enough = data.Inventory.IsEnough(def.LevelStats[currentLevel + 1].price);
        if (enough)
        {
        data.Stats.UpgardeStat(id);
        data.Inventory.Remove(def.LevelStats[currentLevel + 1].price.Id, def.LevelStats[currentLevel + 1].price.Count);
            onStatsChanged?.Invoke(id);
        }  
    }
    public void Select(Characteristics _id)
    {
        selectedStat.Value = _id;
    }
}
