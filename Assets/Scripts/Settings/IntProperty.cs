using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IntProperty : ObservalProperty<int>
{
   public IntProperty(int defaultValue):base(defaultValue)
    {

    }
}
