using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHealthBar : MonoBehaviour
{
    [SerializeField] private ProgressBarWidget HpBar;
    [SerializeField] private HealthComponent healthComponent;
    private int maxHealth;

    private void Start()
    {
        maxHealth = healthComponent.Health;
    }
    public void SetProgress(int hp)
    {
        var progress = (float)hp / maxHealth;
        HpBar.SetProgress(progress);
    }    
}
