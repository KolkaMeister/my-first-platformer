using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject Prefab;
    [SerializeField] private bool usePool;
    [ContextMenu("Spawn")]
    public void Spawn()
    {
        var instanse = usePool
            ?Pool.Instanse.Get(Prefab, spawnPoint.transform.position)
            :Instantiate(Prefab, spawnPoint.transform.position, Quaternion.identity);

        instanse.transform.localScale = spawnPoint.lossyScale;
    }
    public void SetPrefab(GameObject prefab)
    {
        Prefab = prefab;
    }
    public void Spawn(GameObject go)
    {

        var instanse = Instantiate(go, spawnPoint.transform.position, Quaternion.identity);
        instanse.transform.localScale = spawnPoint.lossyScale;
    }
}
