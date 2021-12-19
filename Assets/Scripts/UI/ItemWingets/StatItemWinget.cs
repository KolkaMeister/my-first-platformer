using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatItemWinget : MonoBehaviour,IItemRenderer<StatDef>
{
    [SerializeField] private Text increaseValue;
    [SerializeField] private Text currentValue;
    [SerializeField] private ProgressBarWidget progressBar;
    [SerializeField] private GameObject selector;
    [SerializeField] private Image icon;

    private StatDef def;
    private GameSession session;

    private void Start()
    {
        FindGameSession();
    }
    private void FindGameSession()
    {
        if (session!=null)
        {
            session.statsModel.SelectedStat.OnChanged -= OnSelectedChanded;
        }
        session = FindObjectOfType<GameSession>();
        session.statsModel.SelectedStat.OnChanged += OnSelectedChanded;
    }
    public void SetData(StatDef statDef, int index)
    {
        def = statDef;
        if (session==null)
        {
            FindGameSession();
        }
            UpdateView();
    }
    private void UpdateView()
    {
        icon.sprite = def.Icon;
        var level=session.statsModel.GetStatLevel(def.Id);
        currentValue.text = def.LevelStats[level].value.ToString();
        if (level>=def.MaxLevel)
        {
            increaseValue.text = "MAX";
        }else
        {
            increaseValue.text = def.LevelStats[level + 1].value.ToString();
        }
        var progress = (float)level / def.MaxLevel;
        progressBar.SetProgress(progress);
        OnSelectedChanded(session.statsModel.SelectedStat.Value, default);
    }
    public void OnSelectedChanded(Characteristics _new,Characteristics _old)
    {
        selector.SetActive(def.Id == _new);
    }
    public void Select()
    {
        session.statsModel.Select(def.Id);
    }
}
