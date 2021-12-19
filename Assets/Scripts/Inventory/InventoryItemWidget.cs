using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemWidget : MonoBehaviour,IItemRenderer<InventoryItemData>
{
    [SerializeField]private Image image;
    [SerializeField]private GameObject selection;
    [SerializeField] private Text _value;
    private int index;
    private GameSession session;

    private void Start()
    {
        session=FindObjectOfType<GameSession>();
        session.quickInventory.SelectedIndex.OnChanged += OnIndexChanged;
        OnIndexChanged(session.quickInventory.SelectedIndex.Value, 0);
    }

    public void OnIndexChanged(int newValue, int oldValue)
    {
        selection.SetActive(index ==newValue);
    }



    public void SetData(InventoryItemData item,int _index)
    {
        index = _index;
        var itemDef = DefsFacade.I.ItemDefs.Get(item.Id);
        image.sprite = itemDef.Icon;
        _value.text = item.count.ToString();
    }

    private void OnDestroy()
    {
        if (session.quickInventory!=null)
        {

        session.quickInventory.SelectedIndex.OnChanged -= OnIndexChanged;
        }
    }
}
