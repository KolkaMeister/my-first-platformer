using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExchangeItemWidget : MonoBehaviour,IItemRenderer<ExchangeData>
{
    [SerializeField] private Image reqItemImage;
    [SerializeField] private Image exchangeItemImage;
    [SerializeField] private Text reqItemCount;
    [SerializeField] private Text exchageItemCount;
    [SerializeField] private Button exchangeButton;


    private ExchangeData data;
    private GameSession session;
    private void Start()
    {
    }
    public void FindSessionAndSubscribe()
    {
       session=FindObjectOfType<GameSession>();
       session.data.Inventory.onChange += UpdateView;
    }
    public void SetData(ExchangeData _data,int index=0)
    {
        data=_data;
        if (session==null)
        {
           FindSessionAndSubscribe();
        }
        UpdateView("", 0);
    }
    public void UpdateView(string id,int count)
    {
        var reqDef= DefsFacade.I.ItemDefs.Get(data.ReqItemData.Id);
        var exchangeDef= DefsFacade.I.ItemDefs.Get(data.ExchangeItemData.Id);
        reqItemImage.sprite = reqDef.Icon;
        exchangeItemImage.sprite = exchangeDef.Icon;
        reqItemCount.text = data.ReqItemData.Count.ToString();
        exchageItemCount.text = data.ExchangeItemData.Count.ToString();
        if (data.exchanged)
        {
            exchangeButton.image.color = Color.white;
            exchangeButton.interactable = false;
        }
        else
        {
          var enough = session.data.Inventory.Count(data.ReqItemData.Id)>=data.ReqItemData.Count;
          exchangeButton.image.color = enough ? Color.green : Color.red;
        }
    }
    public void Trade()
    {
        var enough = session.data.Inventory.Count(data.ReqItemData.Id) >= data.ReqItemData.Count;
        if (enough)
        {
            session.data.Inventory.Add(data.ExchangeItemData.Id, data.ExchangeItemData.Count);
            session.data.Inventory.Remove(data.ReqItemData.Id, data.ReqItemData.Count);
            data.exchanged = true;
            exchangeButton.image.color = Color.white;
            exchangeButton.interactable = false;
        }
    }
    private void OnDestroy()
    {
        if (session!=null)
        {
            session.data.Inventory.onChange -= UpdateView;
        }
    }
}
