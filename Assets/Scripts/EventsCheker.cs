using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsCheker : MonoBehaviour
{
    [SerializeField] private UnityEvent onTrue;
    [SerializeField] private UnityEvent onFalse;
    [SerializeField] private CheckList CheckList;
    private  GameSession session;


    private void Start()
    {
        session = FindObjectOfType<GameSession>();
    }
    public void CheckCreatureKilled(string name)
    {
        if (session.data.EventsData.IsCreatureKilled(name))
        {
            onTrue?.Invoke();
        }
        else
        {
            onFalse?.Invoke();
        }
        
    }
    public void CheckEvents(string name)
    {
        if (session.data.EventsData.IsEventHappened(name))
        {
            onTrue?.Invoke();
        }
        else
        {
            onFalse?.Invoke();
        }
    }
    public void ListCheckAny()
    {

        foreach (var creature in CheckList.CreatureList)
        {
            if (session.data.EventsData.IsCreatureKilled(creature)) 
            {
                onTrue?.Invoke();
                return;
            }

        }
        foreach (var _event in CheckList.EventsList)
        {
            if (session.data.EventsData.IsEventHappened(_event))
            {
                onTrue?.Invoke();
                return;
            }
        }
        onFalse?.Invoke();
    }
    public void ListCheckAll()
    {
        foreach (var creature in CheckList.CreatureList)
        {
            if (!session.data.EventsData.IsCreatureKilled(creature))
            {
                onFalse?.Invoke();
                return;
            }
        }
        foreach (var _event in CheckList.EventsList)
        {
            onFalse?.Invoke();
            return;
        }
        onTrue?.Invoke();
    }
}
[Serializable]
public class CheckList
{
    [SerializeField] private List<string> creatureList;
    [SerializeField] private List<string> eventsList;

    public List<string> CreatureList => creatureList;

    public List<string> EventsList => eventsList;

}

