using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyModel 
{

    private PlayerData data;
    public InventoryItemData[] currencyData = new InventoryItemData[0];
    public event Action OnChanged;
    public CurrencyModel(PlayerData _data)
    {
        data = _data;
        currencyData = data.Inventory.GetItems(ItemsTag.Currency);
        data.Inventory.onChange += RebuildCurrency;
    }

    public void RebuildCurrency(string id, int count)
    {
        var def = DefsFacade.I.ItemDefs.Get(id);
        var hasTag = def.HasTag(ItemsTag.Currency);
        if (hasTag)
        {
            var array = data.Inventory.GetItems(ItemsTag.Currency);
            currencyData = array;
            OnChanged?.Invoke();
        }
    }
}
