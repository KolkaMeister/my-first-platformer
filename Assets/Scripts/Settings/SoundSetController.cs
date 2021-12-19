using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundSetController : MonoBehaviour
{
    [SerializeField] SoundSettings setting;
    private AudioSource source;
    private FloatPersistantProperty model;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        model = FindModel();
        model.OnChanged += OnValueChanged;
        OnValueChanged(model.Value, model.Value);
    }
    private FloatPersistantProperty FindModel()
    {
        switch (setting)
        {
            case SoundSettings.Music: 
                return GameSettings.I.Music;
            case SoundSettings.SFX:
                return GameSettings.I.SFX;
        }
        throw new ArgumentException("Undefined mode");
    }
    private void OnValueChanged(float newValue,float oldValue)
    {
        source.volume = newValue;
    }
    private void OnDestroy()
    {
        if (model!=null)
        model.OnChanged -= OnValueChanged;
    }
}
