using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealButtonItemWidget : MonoBehaviour
{
    private GameSession session;
    private void Start()
    {
        session=FindObjectOfType<GameSession>();
        session.quickInventory.OnChanged += OnSelectedChanged;
        session.data.Inventory.onChange += OnInventoryChange;
        OnSelectedChanged();
    }
    public void OnSelectedChanged()
    {

        var def =  DefsFacade.I.ItemDefs.Get(session.quickInventory.SelectedItemID);
        if (def.IsVoid)
        {
            gameObject.SetActive(false);
            return;
        }
        if(def.HasTag(ItemsTag.Healable))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void OnInventoryChange(string id,int count)
    {
        OnSelectedChanged();
    }
    private void OnDestroy()
    {
        if (session.quickInventory!=null)
        {
            session.quickInventory.OnChanged -= OnSelectedChanged;
        }
        if (session.data != null)
        {
            session.data.Inventory.onChange -= OnInventoryChange;
        }
    }
}
