using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickInventoryHud : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private InventoryItemWidget prefab;
    private QuickInventoryModel quickInventory;
    private List<InventoryItemWidget> createdItems= new List<InventoryItemWidget>() ;
    private DataGroup<InventoryItemData, InventoryItemWidget> dataGroup;
    private void Start()
    {
        dataGroup = new DataGroup<InventoryItemData, InventoryItemWidget>(container, prefab);
        quickInventory = FindObjectOfType<GameSession>().quickInventory;
        quickInventory.OnChanged += Rebuild;
        Rebuild();
    }
    public void Rebuild()
    {
        var items = quickInventory.QuickInventory;
        dataGroup.SetData(items);
        //for (int i = createdItems.Count; i <QuickInventory.Length; i++)
        //{
        //    var item = Instantiate(prefab, container);
        //    createdItems.Add(item);
        //}
        //for (int i = 0; i <QuickInventory.Length; i++)
        //{
        //    createdItems[i].SetData(QuickInventory[i], i);
        //    createdItems[i].gameObject.SetActive(true);
        //}
        //for (int i = quickInventory.QuickInventory.Length; i < createdItems.Count; i++)
        //{
        //    createdItems[i].gameObject.SetActive(false);
        //}
    }
    public void NextItem()
    {
        quickInventory.NextItem();
    }
    private void OnDestroy()
    {
        if (quickInventory!=null)
        {
        quickInventory.OnChanged -= Rebuild;
        }
    }

}
