using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeWindow : AnimatedWindow
{
    [SerializeField] private Transform currencyContainer;
    [SerializeField] private Transform exchangeItemContainer;
    [SerializeField] private ExchangeItemWidget itemWidget;
    private PredefinedDataGroup<InventoryItemData, ItemWidget> currencyDataGroup;
    private DataGroup<ExchangeData, ExchangeItemWidget> exchangeDataGroup;
    private ExchangeData[] data;
    private InventoryItemData[] inventoryItems;
    private GameSession session;
    private GeneralUIController controller;
    protected override void Start()
    {
        controller = FindObjectOfType<GeneralUIController>();
        animator = GetComponent<Animator>();
        session = FindObjectOfType<GameSession>();
        session.data.Inventory.onChange += OnInventoryChanged;
        currencyDataGroup = new PredefinedDataGroup<InventoryItemData, ItemWidget>(currencyContainer);
        exchangeDataGroup = new DataGroup<ExchangeData, ExchangeItemWidget>(exchangeItemContainer, itemWidget);
        OnInventoryChanged("",0);
        CloseAction = () =>
              {
                  controller.locker.Dispose(this);
              };
    }
    public void Open(ExchangeData[] _data)
    {
        controller.locker.Retain(this);
        animator.SetBool(IsOpened, true);
        data = _data;
        OnInventoryChanged("", 0);
        exchangeDataGroup.SetData(data);
    }
    public void Open()
    {
        animator.SetBool(IsOpened, true);
    }
    public void OnInventoryChanged(string id,int count)
    {
            inventoryItems=session.data.Inventory.InventoryItems;
            currencyDataGroup.SetData(inventoryItems);
    }
    private void OnDestroy()
    {
        session.data.Inventory.onChange -= OnInventoryChanged;
    }

}
