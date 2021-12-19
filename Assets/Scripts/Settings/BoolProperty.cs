using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BoolProperty : ObservalProperty<bool>
{
      
    public BoolProperty(bool defaultValue):base(defaultValue)
    {

    }
}
