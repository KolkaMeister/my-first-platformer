using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityItemWidget : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button button;
    [SerializeField] private GameObject blocker;
    [SerializeField] private Text realoadTime;

    private AbilityData abilityData; 

    private GameSession session;
    private Hero hero;

    private Coroutine reloadCoroutine;
    void Start()
    {
        session = FindObjectOfType<GameSession>();
        hero = FindObjectOfType<Hero>();
        session.data.Perks.used.OnChanged += OnUsedPerkChanged;
        OnUsedPerkChanged(session.data.Perks.used.Value,"");
    }
    private void OnUsedPerkChanged(string _new,string _old)
    {
        var def = DefsFacade.I.PerksDefs.Get(_new);
        if (def.Id==default)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        icon.sprite = def.Icon;
        if (def.HasTag(PerkType.Active))
        {
            Subscribe(def.Id);
        }
        else
        {
            UnSubscribe();
        }

    }
    private void Subscribe(string _id)
    {
        UnSubscribe();
        abilityData=hero.abilityManager.Get(_id);
        abilityData.abilityUseEvent += OnPerkUsed;
        OnPerkUsed(abilityData.cooldown.RemainedTime);
    }
    private void UnSubscribe()
    {
        if (abilityData != null) abilityData.abilityUseEvent -= OnPerkUsed;
        abilityData = null;
        UnableBlocker();
    }
    private void OnPerkUsed(float remainedTime)
    {
        reloadCoroutine = StartCoroutine(Reload(remainedTime));
        Debug.Log("Call");
    }
    private IEnumerator Reload(float remainedTime)
    {
        button.interactable = false;
        if (reloadCoroutine!=null) UnableBlocker();
        blocker.SetActive(true);
        while (remainedTime > 0)
        {
            remainedTime -= Time.deltaTime;
            realoadTime.text =Math.Round(remainedTime,0).ToString();
            yield return null;
        }
        UnableBlocker();

    }
    private void UnableBlocker()
    {
        if (reloadCoroutine!=null)
        {
       StopCoroutine( reloadCoroutine);
       reloadCoroutine = null;
        }
       blocker.SetActive(false);
        button.interactable = true;
    }
    private void AssignAbility(string id)
    {
        abilityData =hero.abilityManager.Get(id);
        abilityData.abilityUseEvent += OnPerkUsed;
        OnPerkUsed(abilityData.cooldown.RemainedTime);
    }

    public void OnClick()
    {

        abilityData?.Use();
    }
    private void OnDestroy()
    {
        UnSubscribe();
        if (session!=null)
        {
            session.data.Perks.used.OnChanged -= OnUsedPerkChanged;
        }
    }
}

