using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickInventoryModel 
{
    [SerializeField] private PlayerData data;
    private InventoryItemData[] quickInventory;

    public readonly IntProperty SelectedIndex = new IntProperty(0);
    public InventoryItemData[] QuickInventory => quickInventory;
    public event Action OnChanged;
    public InventoryItemData SelectedItem => quickInventory.Length>0?QuickInventory[SelectedIndex.Value]:null;

    public string SelectedItemID => quickInventory.Length > 0 ? QuickInventory[SelectedIndex.Value].Id : String.Empty;
    public QuickInventoryModel(PlayerData _data)
    {
        data = _data;
        quickInventory = data.Inventory.GetItems(ItemsTag.Usable);
        data.Inventory.onChange += OnChangedInventory;
    }
    private void OnChangedInventory(string id,int value)
    {
        var itemDef = DefsFacade.I.ItemDefs.Get(id);
        var hasTag = itemDef.HasTag(ItemsTag.Usable);
        if (hasTag)
        {
            quickInventory = data.Inventory.GetItems(ItemsTag.Usable);
            SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, quickInventory.Length - 1);
            OnChanged?.Invoke();
        }

    }
    public void NextItem()
    {
        SelectedIndex.Value =  (int) Mathf.Repeat(SelectedIndex.Value+1, quickInventory.Length);
    }
   


}
