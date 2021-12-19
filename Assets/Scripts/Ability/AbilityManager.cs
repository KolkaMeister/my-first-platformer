using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager 
{
    public List<AbilityData> abilities=new List<AbilityData>();

    public AbilityData Get(string id)
    {
        foreach (var ability in abilities)
        {
            if (ability.id==id)
            {
                return ability;
            }
        }
        return null;
    }
}
