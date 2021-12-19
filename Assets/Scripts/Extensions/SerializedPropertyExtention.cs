﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public static class SerializedPropertyExtention
{
    public static bool GetEnum<TEnumType>(this SerializedProperty property, out TEnumType retValue)
      where TEnumType:Enum 
    {
        retValue = default;
        var names = property.enumNames;
        if (names == null || names.Length == 0) return false;

        var enumName = names[property.enumValueIndex];
        retValue = (TEnumType)Enum.Parse(typeof(TEnumType), enumName);
        return true;
    }

}
#endif
