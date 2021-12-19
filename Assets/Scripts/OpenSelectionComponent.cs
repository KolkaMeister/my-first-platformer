using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenSelectionComponent : MonoBehaviour
{
    [SerializeField] private SelectionData[] data;

    private SelectionWindow window;
    [ContextMenu("Open")]
    public void OpenSelectionWindow()
    {
        if (window == null) window = FindObjectOfType<SelectionWindow>();

        window.OpenWindow(data);
    }
}
[Serializable]
public struct SelectionData
{
    [SerializeField] private string text;
    [SerializeField] private UnityEvent onSelectAction;

    public string Text=>text;
    public UnityEvent OnSelectAction => onSelectAction;
}

