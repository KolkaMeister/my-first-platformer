using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLightHud : MonoBehaviour
{
    [SerializeField] private Image Icon;
    [SerializeField] private ProgressBarWidget progressBar;
    [SerializeField] private Text value;

    private GameSession session;
    private void Start()
    {
        session = FindObjectOfType<GameSession>();
        session.data.LightFuel.OnChanged+= OnFloatChanged;
        session.data.IsShining.OnChanged += OnBoolChanged;
        OnFloatChanged(session.data.LightFuel.Value,0);
        OnBoolChanged(session.data.IsShining.Value, false);
    }

    public void OnFloatChanged(float newValue,float oldValue)
    {
        progressBar.SetProgress(newValue / 100f);
        value.text = Mathf.Round(newValue).ToString()+"%";
        var color=Color.Lerp(Color.red, Color.yellow, newValue / 100f);
        value.color = color;
    }
    private void OnBoolChanged(bool newValue, bool oldValue)
    {
        Icon.color = session.data.IsShining.Value ? Color.white : Color.black;
    }

    private void OnDestroy()
    {
        if (session.data!=null)
        {
        session.data.LightFuel.OnChanged -= OnFloatChanged;
        session.data.IsShining.OnChanged -= OnBoolChanged;
        }
    }
}
