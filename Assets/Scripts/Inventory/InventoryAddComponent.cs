using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryAddComponent : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private IntProperty count;
    [SerializeField] public UnityEvent OnEmpty;

    public void AddToInventory(GameObject go)
    {
        var Iinterface = go.GetComponent<ICanAddToInvenvoty>();
        if (Iinterface != null)
        {
            Iinterface.AddToInventory(id, count);
        }
    }
    private void Start()
    {
        count.OnChanged += OnValueChange;
    }
    public void OnValueChange(int newValue, int oldValue)
    {
        if (newValue <= 0)
        {
            OnEmpty?.Invoke();
        }
    }
    private void OnDestroy()
    {
        count.OnChanged -= OnValueChange;
    }
}
public interface ICanAddToInvenvoty
{
    void AddToInventory(string id, IntProperty value);
}
