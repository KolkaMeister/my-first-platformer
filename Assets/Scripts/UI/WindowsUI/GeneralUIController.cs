using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GeneralUIController : MonoBehaviour
{
    [SerializeField] private WindowsManager windowManager;
    [Header("MobileButtons")]
    [SerializeField] private GameObject selectButton;
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject windowManagerButton;
    [SerializeField] private GameObject healButton;
    [Header("HeroInput")]
    [SerializeField] private PerksWindow perksWindow;
    [SerializeField] private StatsUpgradeWindow statsWindow;
    [Header("ComponentTrigger")]
    [SerializeField] private ExchangeWindow exchangeWindow;
    [SerializeField] private DialogBoxController dialogController;
    [SerializeField] private SelectionWindow selection;
    [Header("Settings")]
    [SerializeField] private GameObject blocker;
    private AnimatedWindow gameRelatedWindow;
    private static bool isPaused=false;
    public CursorLocker locker=new CursorLocker();
    public Locker InterfaceLocker= new Locker();
    private void Start()
    {
#if MOBILE
        healButton.SetActive(true);
        selectButton.SetActive(true);
        menuButton.SetActive(true);
        windowManagerButton.SetActive(true);
#endif
        locker.Check();
    }
    public void OpenStatsWindow()
    {
        CloseCurrentWindow();
        statsWindow.Open();
        gameRelatedWindow = statsWindow;
    }

    public void OpenPerksWindow()
    {
        CloseCurrentWindow();
        perksWindow.Open();
        gameRelatedWindow = perksWindow;
    }
    public void OpenExchangeWindow(ExchangeData[] data)
    {
        CloseCurrentWindow();
        exchangeWindow.Open(data);
        gameRelatedWindow = exchangeWindow;
    }
    public void OpenWindowManager()
    {
        CloseCurrentWindow();
        windowManager.Open();
        gameRelatedWindow = windowManager;
    }
    public void OpenInGameMenuWindow()
    {
        var window = Resources.Load<GameObject>("UI/Windows/InGameMenu");
        var menuWindow=window.GetComponent<AnimatedWindow>();
        SpawnUtils.CreateWindow<AnimatedWindow>(menuWindow,out var _ins);
        this.Pause();
    }
    public void OpenInGameSettings()
    {
        var window = Resources.Load<GameObject>("UI/Windows/InGameSettings");
        var menuWindow = window.GetComponent<AnimatedWindow>();
        SpawnUtils.CreateWindow<AnimatedWindow>(menuWindow, out var _ins);
    }
    public void OpenInGameLocaleWindow()
    {
        var window = Resources.Load<GameObject>("UI/Windows/InGameLocalization");
        var menuWindow = window.GetComponent<AnimatedWindow>();
        SpawnUtils.CreateWindow<AnimatedWindow>(menuWindow, out var _ins);
    }
    public void OpenSelectionWindow(SelectionData[] data)
    {
        CloseCurrentWindow();
        selection.OpenWindow(data);
    }
    public void StartDialog(DialogData data)
    {
        CloseCurrentWindow();
        dialogController.StartDialog(data);
    }
    public void CloseCurrentWindow()
    {
        if (gameRelatedWindow!=null)
        {
            gameRelatedWindow.Close();
            gameRelatedWindow = null;
        }
    }
    public void Resume()
    {
        isPaused = false;
        locker.Dispose(this);
        Time.timeScale = 1f;
        blocker.SetActive(false);
    }
    public void Pause()
    {
        isPaused = true;
        locker.Retain(this);
        Time.timeScale = 0;
        blocker.SetActive(true);
    }






    public void Menu(InputAction.CallbackContext context)
    {
        //if (context.performed)
        //{
        //    Debug.Log(MenuWindow);
        //    if (MenuWindow!=null)
        //    {
        //        if (MenuWindow is InGameMenu)
        //        {
        //            ((InGameMenu)MenuWindow).Resume();
        //        }
        //        else
        //        {
        //            Debug.Log("This");
        //            MenuWindow.Close();
        //            OpenInGameMenuWindow();
        //        }
        //    }else
        //    {
        //    OpenInGameMenuWindow();
        //    }
        //}
        if (context.canceled)
        {
            if (!isPaused)
            {
                OpenInGameMenuWindow();
            }
        }
    }
}
