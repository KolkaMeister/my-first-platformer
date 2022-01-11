using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocaleWindow : AnimatedWindow
{
    [SerializeField] private LocaleItemWidget itemWidget;
    [SerializeField] private GameObject container;

    private DataGroup<string, LocaleItemWidget> dataGroup;
    protected override void Start()
    {
        base.Start();
        dataGroup = new DataGroup<string, LocaleItemWidget>(container.transform, itemWidget);
        SpawnLocaleItems();
    }
    private void SpawnLocaleItems()
    {
        var keys = LocalizationManager.LocaleNames.ToArray();
        dataGroup.SetData(keys);
    }
    public void BackToMenu()
    {
        CloseAction = () =>
          {
              var window = Resources.Load<GameObject>("UI/Windows/MainMenu");
              SpawnUtils.CreateWindow<GameObject>(window, out var _ins);
              Destroy(gameObject);
          };
        Close();
    }
}
