using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PersistantProperty<TPropertyType>
{
    [SerializeField] private TPropertyType _value;
    private TPropertyType stored;
    public TPropertyType defaultValue;
    public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);
    public event OnPropertyChanged OnChanged;
    public PersistantProperty(TPropertyType DefaultValue)
    {
        defaultValue = DefaultValue;
    }
    protected abstract void Write(TPropertyType value);
    protected abstract TPropertyType Read(TPropertyType value);

    public void Init()
    {

        stored=_value = Read(defaultValue);
    }
    public TPropertyType Value
    {
        get => stored;

        set
        {
           var equals= stored.Equals(value);
            if (equals) return;
            var oldValue = _value;
            Write(value);
            stored = _value = value;
            OnChanged?.Invoke(value, oldValue);
        }
    }
    public void Validate()
    {
        if (!stored.Equals(_value))
        {
            Value = _value;
        }
    }
}
