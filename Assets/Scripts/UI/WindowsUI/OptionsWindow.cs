using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsWindow : AnimatedWindow
{
    [SerializeField] SoundSettingsWidget Music;
    [SerializeField] SoundSettingsWidget SFX;
    protected override void Start()
    {
        base.Start();
        Music.SetModel(GameSettings.I.Music);
        SFX.SetModel(GameSettings.I.SFX);
    }
    public void BackToMenu()
    {
        CloseAction = () =>
          {
              var window = Resources.Load<GameObject>("UI/Windows/MainMenu");
              SpawnUtils.CreateWindow<GameObject>(window, out var _int);
          };
        Close();
    }

}
