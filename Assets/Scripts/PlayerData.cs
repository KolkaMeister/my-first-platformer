using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData 
{
    [SerializeField] private InventoryData inventory;
    [SerializeField] private PerksData perks;
    [SerializeField] private HeroStatsData stats;
    [SerializeField] private EventsData eventsData;

    public InventoryData Inventory => inventory;

    public PerksData Perks => perks;

    public EventsData EventsData => eventsData;

    public HeroStatsData Stats => stats;

    [SerializeField] private IntProperty hp= new IntProperty(10);

    [SerializeField] private FloatProperty lightFuel = new FloatProperty(100f);

    [SerializeField] private BoolProperty isShining = new BoolProperty(false);
    public IntProperty Hp => hp;
    public FloatProperty LightFuel => lightFuel;

    public BoolProperty IsShining => isShining;

    public PlayerData Clone()
    {
        var json = JsonUtility.ToJson(this);
        return JsonUtility.FromJson<PlayerData>(json);
    }
}
