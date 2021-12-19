using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class ObservalProperty<TPropertyType>
{
    [SerializeField] protected TPropertyType _value;
    public delegate void OnValueChanged(TPropertyType newValue, TPropertyType oldValue);
    public event OnValueChanged OnChanged;

    public ObservalProperty(TPropertyType defaultValue)
    {
        _value = defaultValue;
    }
    public TPropertyType Value
    {
        get => _value;

        set
        {
            var equals = _value.Equals(value);
            if (equals) return;
            var oldValue = _value;
            _value = value;
            OnChanged?.Invoke(_value, oldValue);
        }


    }
}
