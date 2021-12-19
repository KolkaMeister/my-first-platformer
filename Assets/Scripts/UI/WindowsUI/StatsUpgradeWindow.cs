using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUpgradeWindow : AnimatedWindow
{
    [SerializeField] private Transform currencyContainer;
    [SerializeField] private Transform statsContainer;
    [SerializeField] private StatItemWinget statItemPrefab;
    [SerializeField] private Button upgrade;
    [SerializeField] private ItemWidget priceWidget;

    private DataGroup<StatDef, StatItemWinget> statsDataGroup;
    private PredefinedDataGroup<ItemDef, CurrencyItemWidget> currencyDataGroup;
    private GameSession session;
    private GeneralUIController controller;


    public CharacteristicsProperty SelectedStat => session.statsModel.SelectedStat;
    protected override void Start()
    {
        controller = FindObjectOfType<GeneralUIController>();
        CloseAction = () =>
          {
              controller.locker.Dispose(this);
          };
        animator = GetComponent<Animator>();
        session = FindObjectOfType<GameSession>();
        statsDataGroup = new DataGroup<StatDef, StatItemWinget>(statsContainer, statItemPrefab);
        currencyDataGroup = new PredefinedDataGroup<ItemDef, CurrencyItemWidget>(currencyContainer);
        session.statsModel.onStatsChanged += Rebuild;
        session.statsModel.SelectedStat.OnChanged += OnSelectedChange;
        upgrade.onClick.AddListener(UpgradeStat);
    }
    [ContextMenu("open")]
     public void Open()
    {
        controller.locker.Retain(this);
        animator.SetBool(IsOpened, true);
        var currencyDefs = DefsFacade.I.ItemDefs.GetTagget(ItemsTag.Currency);
        currencyDataGroup.SetData(currencyDefs);
        Rebuild(Characteristics.Hp);
    }
    

    public void OnSelectedChange(Characteristics _new, Characteristics _old)
    {
        var def= DefsFacade.I.HeroStatsDef.Get(SelectedStat.Value);
        var nextLevel = session.statsModel.GetStatLevel(SelectedStat.Value)+1;
        if (nextLevel>def.MaxLevel)
        {
            upgrade.gameObject.SetActive(false);
            priceWidget.gameObject.SetActive(false);
        }
        else
        {
            upgrade.gameObject.SetActive(true);
            priceWidget.gameObject.SetActive(true);
            var _price = def.LevelStats[nextLevel].price;
            priceWidget.SetData(new InventoryItemData(_price.Id, 0) { count = _price.Count }, 0);
            var enough = session.data.Inventory.IsEnough(_price);
            upgrade.image.color = enough ? Color.green : Color.red;
            upgrade.interactable = enough;
        }    
    }
    public void Rebuild(Characteristics id)
    {
        var statsDefs = DefsFacade.I.HeroStatsDef.GetAll;
        statsDataGroup.SetData(statsDefs);
        OnSelectedChange(SelectedStat.Value, default);
    }
    public void UpgradeStat()
    {
        session.statsModel.UpgradeStat(SelectedStat.Value);
    }
    
}
