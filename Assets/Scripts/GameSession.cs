using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Analytics;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int leveIndex;
    [SerializeField] public PlayerData data;
    public PlayerData save;
    public string checkPointId;
    public event Action OnCheckPointChanged;
    public QuickInventoryModel quickInventory { get; private set; }
    public PerksModel perksModel { get; private set; }

    public StatsModel statsModel { get; private set; }

    private void Awake()
    {
        SpawnHud();
        var session = IsGameSessionExist();
        if (session != null)
        {
            session.SendLevelStartEvent(leveIndex);
            session.InitModels();
            SpawnCameraEffects();
            session.SpawnHero();
            Destroy(gameObject);
        }
        else
        {
            SendLevelStartEvent(leveIndex);
            SpawnCameraEffects();
            InitModels();
            SpawnHero();
            this.Save();
            DontDestroyOnLoad(this);
        }
    }
    private GameSession IsGameSessionExist()
    {

        var session = FindObjectOfType<GameSession>();
        if (session != this)
        {
            return session;
        }
        else
        return null;
    }
    public void SendLevelStartEvent(int index)
    {
        var eventParams = new Dictionary<string, object>()
        {
            { "level_index",index }
        };
        AnalyticsEvent.Custom("level_start", eventParams);
    }
    public void SpawnCameraEffects()
    {
        Instantiate(Resources.Load<GameObject>("UI/CameraEffectCanvas")); 
    }
    private void InitModels()
    {
        quickInventory = new QuickInventoryModel(data);
        perksModel = new PerksModel(data);
        statsModel = new StatsModel(data);
    }
    private void SpawnHud()
    {
        var hud = Resources.Load<GameObject>("UI/HudCanvas");
        var i = Instantiate(hud);
#if MOBILE
        LoadControls();
#endif
    }
#if MOBILE

    public void LoadControls()
    {
        var go = Resources.Load<GameObject>("UI/MobileControlsCanvas");
        Instantiate(go);
    }
#endif
    public void Save()
    {
        save = data.Clone();
    }
    public void LoadSaveLast()
    {
        data = save.Clone();
    }
    public void SetCheckPointId(string Id)
    {
        checkPointId = Id;
        OnCheckPointChanged();
        Save();
    }
    [ContextMenu("SpawnHero")]
    private void SpawnHero()
    {
        var checkPoints = FindObjectsOfType<CheckPointComponent>();
        var CheckPoint=FindCheckPoint(checkPointId, checkPoints);
        if (CheckPoint==null)
        CheckPoint= FindCheckPoint("default", checkPoints);
        CheckPoint.spawner.Spawn(CharacterSettingsManager.I.GetCharacter());
    }
    private CheckPointComponent FindCheckPoint(string Id, CheckPointComponent[] points)
    {
        foreach (var point in points)
        {
            if (point.Id == Id)
            {
                return point;
            }
        }
        return null;
    }
    public void ReloadLevel()
    {
        var reloader =FindObjectOfType<ReloadLevelComponent>();
        reloader.ReloadLevel();
    }
}
