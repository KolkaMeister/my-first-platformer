using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Defs/DefsFacade" , fileName="DefsFacade")]
public class DefsFacade : ScriptableObject
{
    [SerializeField] private InventoryItemsDefs itemDefs;
    [SerializeField] private PlayerDefs player;
    [SerializeField] private ThrowableItemDefs throwableItemDefs;
    [SerializeField] private HealItemsDef healItemsDef;
    [SerializeField] private PerksDefs perksDefs;
    [SerializeField] private HeroStatsDef heroStatsDef;
    public InventoryItemsDefs ItemDefs=> itemDefs;
    public PlayerDefs Player => player;
    public ThrowableItemDefs ThrowableItemDefs => throwableItemDefs;
    public HealItemsDef HealItemsDef=> healItemsDef;

    public HeroStatsDef HeroStatsDef => heroStatsDef;

    public PerksDefs PerksDefs => perksDefs;

    private static DefsFacade instance;
    public static DefsFacade I => instance == null ? LoadDefs() : instance;
    private static DefsFacade LoadDefs()
    {
        return instance = Resources.Load<DefsFacade>("DefsFacade");
    }
}
