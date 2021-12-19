using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PrefsPersistantProperty<TPropertyType> : PersistantProperty<TPropertyType>
{
    protected string key;
    public PrefsPersistantProperty(TPropertyType DefaultValue,string Key):base(DefaultValue)
    {
        key = Key;
    }
}
