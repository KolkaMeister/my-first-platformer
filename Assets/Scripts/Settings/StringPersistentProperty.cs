using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringPersistentProperty : PrefsPersistantProperty<string>
{
    public StringPersistentProperty (string defaultValue,string key):base(defaultValue, key)
    {
        Init();
    }
    protected override string Read(string defaultyValue)
    {
        return PlayerPrefs.GetString(key,defaultyValue);
    }
    protected override void Write(string value)
    {
        PlayerPrefs.SetString(key, value);
    }
}
