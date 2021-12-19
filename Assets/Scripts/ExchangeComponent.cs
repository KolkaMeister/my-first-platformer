using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeComponent : MonoBehaviour
{
    [SerializeField] private ExchangeData[] exchangeData;
    private GeneralUIController controller;

    [ContextMenu("OpenExchangeWindow")]
    public void OpenExchangeWindow()
    {
        FindExchangeWindiow();
        controller.OpenExchangeWindow(exchangeData);

    }
    public void FindExchangeWindiow()
    {
        if (controller != null) return;
        controller = FindObjectOfType<GeneralUIController>();
    }




}
[Serializable]
public class ExchangeData
{
    [SerializeField] private ItemData reqItemData;
    [SerializeField] private ItemData exchangeItemData;
    [SerializeField] public bool exchanged=false;

    public ItemData ReqItemData => reqItemData;
    public ItemData ExchangeItemData => exchangeItemData;
}
[Serializable]
public class ItemData
{
    [SerializeField] private string id;
    [SerializeField] private int count;

    public string Id => id;
    public int Count => count;

}


