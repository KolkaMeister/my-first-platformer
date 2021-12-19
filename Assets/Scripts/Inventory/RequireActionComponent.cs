using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RequireActionComponent : MonoBehaviour
{
    [SerializeField] private InventoryItemData[] require;
    [SerializeField] private bool remove;

    [SerializeField] UnityEvent onSuccess;
    [SerializeField] UnityEvent onFail;

    public void Check()
    {
        var gameSession = FindObjectOfType<GameSession>();
        bool areAllRequirementsMet = true;
        foreach (var item in require)
        {
            var numItems = gameSession.data.Inventory.Count(item.Id);
            if (numItems < item.count) areAllRequirementsMet = false;
        }
        if (areAllRequirementsMet)
        {
            if (remove)
            {
                foreach (var item in require)
                {
                    gameSession.data.Inventory.Remove(item.Id, item.count);
                }
            }
            onSuccess?.Invoke();
        }
        else
        {
            onFail?.Invoke();
        }
    }
}
