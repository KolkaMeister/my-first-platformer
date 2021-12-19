using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class FloatPersistantProperty : PrefsPersistantProperty<float>
{
    public FloatPersistantProperty(float deafaultValue, string key) : base(deafaultValue, key)
    {
        Init();
    }
    protected override float Read(float defaultValue)
    {
       return PlayerPrefs.GetFloat(key, defaultValue);
    }
    protected override void Write(float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }
}
