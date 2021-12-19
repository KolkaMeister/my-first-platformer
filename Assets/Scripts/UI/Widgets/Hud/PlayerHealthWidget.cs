using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthWidget : MonoBehaviour
{
    [SerializeField] private ProgressBarWidget HpBar;
    private int maxHealth;
    private GameSession gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        gameSession.data.Hp.OnChanged += OnValueChange;
        gameSession.statsModel.onStatsChanged += OnStatsChaged;
        maxHealth = gameSession.statsModel.GetStatValue(Characteristics.Hp);
        OnValueChange(gameSession.data.Hp.Value, gameSession.data.Hp.Value);
    }
    public void OnStatsChaged(Characteristics _id)
    {
        if (_id==Characteristics.Hp)
        {
            maxHealth = gameSession.statsModel.GetStatValue(Characteristics.Hp);
            OnValueChange(gameSession.data.Hp.Value, maxHealth);
        }
    }
    public void OnValueChange(int newValue,int oldValue)
    {
        var progress = (float)newValue / maxHealth;
        HpBar.SetProgress(progress);

    }
    private void OnDestroy()
    {
        if (gameSession.data != null)
        {
            gameSession.data.Hp.OnChanged -= OnValueChange;
        }
        if (gameSession.statsModel != null)
        {
            gameSession.statsModel.onStatsChanged -= OnStatsChaged;
        }
    }
}
