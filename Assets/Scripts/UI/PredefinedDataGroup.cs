using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredefinedDataGroup<TDataType, TItemWidget> : DataGroup<TDataType, TItemWidget>
    where TItemWidget : MonoBehaviour, IItemRenderer<TDataType>
{
    public PredefinedDataGroup(Transform _container) : base(_container, null)
    {
        container = _container;
        UpdateGroup();
    }
    private void UpdateGroup()
    {
        var Items = container.GetComponentsInChildren<TItemWidget>();
        createdItems = new List<TItemWidget>();
        createdItems.AddRange(Items);
    }
    public override void SetData(TDataType[] data)
    {
        for (int i = 0; i < data.Length&& i< createdItems.Count; i++)
        {
            createdItems[i].gameObject.SetActive(true);
            createdItems[i].SetData(data[i],i);
        }
        for (int i = data.Length; i < createdItems.Count; i++)
        {
            createdItems[i].gameObject.SetActive(false);
        }
    }

}
