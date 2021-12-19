using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PerksWindow : AnimatedWindow
{
    [SerializeField] private Transform container;
    [SerializeField] private Button Use;
    [SerializeField] private Button Buy;
    [SerializeField] private ItemWidget itemWidget;
    [SerializeField] private Text info;
    private GameSession session;
    private PredefinedDataGroup<string, PerkItemWidget> dataGroup;
    private GeneralUIController controller;
    public string Selected => session.perksModel.Selected;
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        session = FindObjectOfType<GameSession>();
        session.perksModel.OnChanged += Rebuild;
        Use.onClick.AddListener(UsePerk);
        Buy.onClick.AddListener(BuyPerk);

        controller = FindObjectOfType<GeneralUIController>();
        CloseAction = () =>
          {
              controller.locker.Dispose(this);
          };
    }
    [ContextMenu("Open")]
    public  void Open()
    {
        controller.locker.Retain(this);
        animator.SetBool(IsOpened, true);
        dataGroup = new PredefinedDataGroup<string, PerkItemWidget>(container);
        Rebuild();
    }
    public void Rebuild()
    {
        var data = DefsFacade.I.PerksDefs.GetAllID();
        dataGroup.SetData(data);
        UpdateInteractablePanel();
    }
    private void UpdateInteractablePanel()
    {
        var def = DefsFacade.I.PerksDefs.Get(Selected);
        info.text = def.Info;
        var isUnlocked = session.perksModel.IsUnlocked(Selected);
        Buy.gameObject.SetActive(!isUnlocked);
        var enough = session.perksModel.IsEnough(Selected);
        Buy.image.color = enough ? Color.green : Color.red;
        Buy.interactable = enough;
        Use.gameObject.SetActive(session.perksModel.IsUnlocked(Selected));
        Use.interactable = !session.perksModel.IsUsed(Selected);
        if (!isUnlocked && !string.IsNullOrEmpty(Selected))
        {
            var icon = DefsFacade.I.ItemDefs.Get(def.Price.Id).Icon;
            itemWidget.gameObject.SetActive(true);
            itemWidget.count.text = def.Price.Count.ToString();
            itemWidget.image.sprite = icon;
        }
        else
        {
            itemWidget.gameObject.SetActive(false);
        }

    }

    public void BuyPerk()
    {
        session.perksModel.Unlock(Selected);
    }
    public void UsePerk()
    {
        session.perksModel.Use(Selected);
    }
    private void OnDestroy()
    {
        if (session.perksModel!=null)
        {
            session.perksModel.OnChanged -= Rebuild;
        }
    }

}
