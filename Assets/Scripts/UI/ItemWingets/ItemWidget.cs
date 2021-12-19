using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWidget :MonoBehaviour,IItemRenderer<InventoryItemData>
{
    [SerializeField] public Text count;
    [SerializeField] public Image image;
    public void SetData(InventoryItemData itemData,int index)
    {
       var def=DefsFacade.I.ItemDefs.Get(itemData.Id);
        image.sprite = def.Icon;
        count.text = itemData.count.ToString();

    }
}


