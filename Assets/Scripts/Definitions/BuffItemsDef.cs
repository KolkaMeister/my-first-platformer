using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItemsDef : ScriptableObject
{
    
}
[Serializable]
public abstract class BuffItem
{
    [SerializeField] protected float value;
    [SerializeField] protected float dutaration;
    public abstract void Use(Hero hero);

    public float Value => value;
}
public class SpeedBufItem:BuffItem
{
    public override void Use(Hero hero)
    {
        hero.SpeedBuf(value, dutaration);
    }

}

