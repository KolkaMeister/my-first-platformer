using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EventsData
{

    [SerializeField] private List<string> events = new List<string>();
    [SerializeField] private List<string> killedСreature = new List<string>();


    public void AddCreature(string _name)
    {
        if (killedСreature.Contains(_name)) return;
        killedСreature.Add(_name);
    }

    public bool IsCreatureKilled(string _name)
    {
        if (killedСreature.Contains(_name)) return true;
        return false;

    }
    public void AddEvent(string _name)
    {
        if (events.Contains(_name)) return;
        killedСreature.Add(_name);
    }
    public bool IsEventHappened(string _name)
    {
        if (events.Contains(_name)) return true;
        return false;
    }

}
