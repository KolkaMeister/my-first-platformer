using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectionItemWindget : MonoBehaviour,IItemRenderer<SelectionData>
{
    [SerializeField] private Text text;
    public UnityEvent selectAction;


    private SelectionData data;
    public void SetData(SelectionData _data,int index)
    {
        data = _data;
        selectAction = data.OnSelectAction;
        text.text =$"{(index+1).ToString()}."+data.Text;
    }
    public void OnSelect()
    {
        selectAction?.Invoke();
        FindObjectOfType<SelectionWindow>().Close();
    }
}
