using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsWidget : MonoBehaviour
{
    [SerializeField] SoundSettings setting;
    [SerializeField] Slider slider;
    [SerializeField] Text text;
    [SerializeField] FloatPersistantProperty model;
    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }
    public void SetModel(FloatPersistantProperty property)
    {
        model = property;
        model.OnChanged += OnValueChanged;
       OnValueChanged(model.Value, model.Value);
    }
    private void OnSliderValueChanged(float value)
    {
        model.Value = value;
    }
    public void OnValueChanged(float newValue,float oldValue)
    {
        slider.value = newValue;
        var textValue=Mathf.Round(newValue * 100);
        text.text = textValue.ToString();
    }
    private void OnDestroy()
    {
        slider.onValueChanged.RemoveAllListeners();
        model.OnChanged -= OnValueChanged;
    }
}
