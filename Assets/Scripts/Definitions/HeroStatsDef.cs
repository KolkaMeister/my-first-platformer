using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/HeroStatsDef", fileName = "HeroStatsDef")]
public class HeroStatsDef : ScriptableObject
{
    [SerializeField] private StatDef[] stats;
    public StatDef[] Stats => stats;

    public StatDef Get(Characteristics id)
    {
        foreach (var stat in Stats)
        {
            if (id == stat.Id)
            {
                return stat;
            }
        }
        return default;
    }

    public StatDef[] GetAll => Stats;


}
[Serializable]
public struct StatDef
{
    [SerializeField] private Characteristics id;
    [SerializeField] private Sprite icon;
    [SerializeField] private LevelStats[] levelStats;

    public int MaxLevel => levelStats.Length - 1;
    public Characteristics Id => id;
    public Sprite Icon => icon;

    public LevelStats[] LevelStats => levelStats;
}
public enum Characteristics
{ 
Hp,
Damage,
Speed,
}
[Serializable]
public struct LevelStats
{
    [SerializeField] public int value;
    [SerializeField] public ItemWithCount price;

}



