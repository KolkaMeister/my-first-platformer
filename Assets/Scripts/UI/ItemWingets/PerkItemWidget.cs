using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkItemWidget : MonoBehaviour,IItemRenderer<string>
{
    
    [SerializeField] private GameObject selector;
    [SerializeField] private Image Icon;
    [SerializeField] private GameObject locked;
    [SerializeField] private GameObject used;
    private string id;
    private GameSession session;

    private void Start()
    {
        session = FindObjectOfType<GameSession>();
    }
    public void  SetData(string data,int index)
    {
        id = data;
        if (session != null)
            UpdateView();
        
    }
    private void UpdateView()
    {
        var def= DefsFacade.I.PerksDefs.Get(id);
        Icon.sprite = def.Icon;

        selector.SetActive(session.perksModel.IsSelected(id));
        locked.SetActive(!session.perksModel.IsUnlocked(id));
        used.SetActive(session.perksModel.IsUsed(id));
        
    }
    [ContextMenu("Select")]
    public void Select()
    {
        session.perksModel.Select(id);
    }
}
