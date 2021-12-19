using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameOptions : AnimatedWindow
{
    [SerializeField] SoundSettingsWidget Music;
    [SerializeField] SoundSettingsWidget SFX;
    protected override void Start()
    {

        base.Start();
        CloseAction = () =>
         {
             Destroy(gameObject);
         };
        Music.SetModel(GameSettings.I.Music);
        SFX.SetModel(GameSettings.I.SFX);

    }
    public void BackToMenu()
    {
        CloseAction = () =>
          {
              FindObjectOfType<GeneralUIController>().OpenInGameMenuWindow();
              Destroy(gameObject);
          };
        Close();
    }


}
