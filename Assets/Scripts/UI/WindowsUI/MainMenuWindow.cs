using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuWindow : AnimatedWindow
{
    public void StartGame()
    {
        CloseAction = () =>
        {
            FindObjectOfType<LevelLoader>().LoadScene("Level 1");
        };
        Close();
        
    }
    public void OpenOptions()
    {
        CloseAction = () =>
        {
            var window = Resources.Load<GameObject>("UI/Windows/SettingsWindow");
            SpawnUtils.CreateWindow<GameObject>(window,out var _ins);
            Destroy(gameObject);
        };
        Close();
    }
    public void ExitGame()
    {
        CloseAction = () =>
          {
#if Unity_Editor
            UnityEditor.EditorApplication.isPlaying=false;
#endif
            Application.Quit();
          };
        Close();

    }
    public void LanguageSettings()
    {
        CloseAction = () =>
        {
        var window = Resources.Load<GameObject>("UI/Windows/LocaleWindow");
        SpawnUtils.CreateWindow<GameObject>(window, out var _ins);
        Destroy(gameObject);
        };
        Close();
    }
    public override void OnCloseAnimationComplete()
    {
        base.OnCloseAnimationComplete();
    }
} 
