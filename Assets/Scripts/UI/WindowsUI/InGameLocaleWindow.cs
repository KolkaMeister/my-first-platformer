using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameLocaleWindow : AnimatedWindow
{
    [SerializeField] private LocaleItemWidget itemWidget;
    [SerializeField] private GameObject container;

    private List<LocaleItemWidget> spawnedItems = new List<LocaleItemWidget>();

    private DataGroup<string, LocaleItemWidget> dataGroup;
    protected override void Start()
    {
        base.Start();
        CloseAction = () =>
        {
            Destroy(gameObject);
        };
        animator.SetBool(IsOpened, true);
        dataGroup = new DataGroup<string, LocaleItemWidget>(container.transform, itemWidget);
        SpawnLocaleItems();
    }
    private void SpawnLocaleItems()
    {
        var names = LocalizationManager.LocaleNames.ToArray();
        dataGroup.SetData(names);
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
