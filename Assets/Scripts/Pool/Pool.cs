using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private Dictionary<int, Queue<PoolItem>> items = new Dictionary<int, Queue<PoolItem>>();

    private static Pool instance;

    public  static Pool Instanse
    {
        get
        {
            if (instance != null)return instance;
            return instance = new GameObject("***MAINPOOL*").AddComponent<Pool>();

        }
    
    }
    public GameObject Get(GameObject go,Vector3 position)
    {
        var id = go.GetInstanceID();
        items.TryGetValue(id, out var queue);
        if (queue==null)
        {
            queue = new Queue<PoolItem>();
            items.Add(id, queue);
        }
        var poolItem = GetFromQueue(go, queue);


        poolItem.transform.position = position;
        poolItem.gameObject.SetActive(true);
        poolItem.Release();
        return poolItem.gameObject;
    }
    public PoolItem GetFromQueue(GameObject go, Queue<PoolItem> queue)
    {
            if (queue.Count<=0)
            {
              var id  =  go.GetInstanceID();
              var item = Instantiate(go, this.transform);
              var poolItem = item.GetComponent<PoolItem>();
              poolItem.SetData(id, this);
              return poolItem;
            }


        return queue.Dequeue();

    }

    public void ReturnToPool (int _id,PoolItem item)
    {
         item.gameObject.SetActive(false);
         items.TryGetValue(_id,out var queue);
         queue.Enqueue(item);
    }
   
   






       
}
