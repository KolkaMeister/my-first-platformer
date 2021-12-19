using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnListComponent : MonoBehaviour
{
    [SerializeField] SpawnData[] spawners;
    public void Spawn(string id)
    {
        foreach (var spawner in spawners)
        {
            if (spawner.id==id)
            {
                spawner.component.Spawn();
                break;
            }
        }
    }


    [Serializable]
    public class SpawnData
    {
        public string id;
        public SpawnComponent component ;
    }
}
