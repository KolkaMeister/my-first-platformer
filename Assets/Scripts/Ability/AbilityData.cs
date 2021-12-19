using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityData
{
    public string id;
    public Cooldown cooldown;
    public delegate bool Ability();
    public Ability ability;
    public event Action<float> abilityUseEvent;

    public AbilityData(Ability _ability,float resetTime,string _id)
    {
        ability = _ability;
        cooldown = new Cooldown(resetTime);
        id = _id;
    }
    public void Use()
    {
        if (ability.Invoke())
        {
            Debug.Log("AbilityUSED!!!");
            cooldown.Reset();
            abilityUseEvent?.Invoke(cooldown.RemainedTime);
        }
    }
    
}
