using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HeroStatsData 
{
    [SerializeField] private List<Stat> stats = new List<Stat>();

    public Stat Get(Characteristics id)
    {
        foreach (var stat in stats)
        {
            if (stat.Id==id)
            {
                return stat;
            }
        }
        return default;
    }
    public void UpgardeStat(Characteristics _id)
    {
        foreach (var stat in stats)
        {
            if (stat.Id==_id)
            {
                stat.level++;
                return;
            }
        }
        stats.Add(new Stat { id = _id, level = 1 });
    }
    public int GetStats(Characteristics id)
    {
        var def = DefsFacade.I.HeroStatsDef.Get(id);
        foreach (var stat in stats)
        {
            if (stat.Id==id)
            {
                return def.LevelStats[stat.Level].value;
            }
        }
        return def.LevelStats[0].value;
    }

    public int GetStatLevel(Characteristics id)
    {
        foreach (var stat in stats)
        {
            if (stat.Id==id)
            {
                return stat.Level;
            }
        }
        return 0;
    }


}
[Serializable]
public class Stat
{
    [SerializeField]public Characteristics id;
    [SerializeField]public int level;

    public Characteristics Id => id;

    public int Level => level;
    
}

