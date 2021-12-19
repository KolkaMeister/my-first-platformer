using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : AnimatedWindow
{
    protected override void Start()
    {
        base.Start();
        CloseAction = () =>
          {
              Destroy(gameObject);
          };
    }
    public void Resume()
    {
        Close();
        CloseAction = () =>
          {
              Destroy(gameObject);
              FindObjectOfType<GeneralUIController>().Resume();
          };

    }
    public void OpenLocaleWindow()
    {
        Close();
        CloseAction = () =>
        {
            Destroy(gameObject);
            FindObjectOfType<GeneralUIController>().OpenInGameLocaleWindow();
        };
    }
    public void OpenSettingsWindow()
    {
        Close();
        CloseAction = () =>
        {
            Destroy(gameObject);
            FindObjectOfType<GeneralUIController>().OpenInGameSettings();
        };
    }
    public void ToMainMenu()
    {
        CloseAction = () =>
        {
            FindObjectOfType<LevelLoader>().LoadScene("MainMenu");
        };
        Close();
    }

}
