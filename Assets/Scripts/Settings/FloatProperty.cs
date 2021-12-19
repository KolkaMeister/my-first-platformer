using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FloatProperty : ObservalProperty<float>
{
    public FloatProperty(float defaultValue) : base(defaultValue)
    {

    }
}
