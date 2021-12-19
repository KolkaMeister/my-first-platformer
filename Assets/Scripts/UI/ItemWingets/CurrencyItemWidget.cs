using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CurrencyItemWidget : MonoBehaviour,IItemRenderer<ItemDef>
{
    [SerializeField] private Image icon;
    [SerializeField] private Text count;
    private GameSession session;
    private ItemDef def;
    void Start()
    {
        session = FindObjectOfType<GameSession>();
    }
    public void SetData(ItemDef _def,int index)
    {
        def = _def;
        Sub();
        UpdateView();
    }
    public void Sub()
    {
        session.data.Inventory.onChange -= OnValueChange;
        session.data.Inventory.onChange += OnValueChange;
    }
    public void UpdateView()
    {
        icon.sprite = def.Icon;
        OnValueChange(def.Id, session.data.Inventory.Count(def.Id));
    }
    public void OnValueChange(string id,int _count)
    {
        if (id==def.Id)
        {
            count.text = _count.ToString();
        }
    }
    private void OnDestroy()
    {
        if (session.data.Inventory!=null)
        {
            session.data.Inventory.onChange -= OnValueChange;
        }
    }
}
