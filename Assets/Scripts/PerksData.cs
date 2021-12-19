using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PerksData 
{
    public StringProperty used = new StringProperty("");

    [SerializeField]private List<string> unlockedPerks = new List<string>();

    public bool SuperThrow => used.Value == "super_throw";
    public bool DivineShield => used.Value == "divine_protection";
    public bool DoubleJump => used.Value == "double_jump";

    public string Used => used.Value;


    public void Add(string id)
    {
        if (!unlockedPerks.Contains(id))
        {
            unlockedPerks.Add(id);
        }
    }
    public bool IsUnlocked(string id)
    {
        return unlockedPerks.Contains(id);
    }
    public void SetUsed(string id)
    {
        used.Value = id;
    }
    

}
