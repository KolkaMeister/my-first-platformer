using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Defs/PlayerDefs",fileName ="PlayerDefs")]
public class PlayerDefs : ScriptableObject
{
    [SerializeField] private int maxHealth;
    public int MaxHealth => maxHealth;
}
