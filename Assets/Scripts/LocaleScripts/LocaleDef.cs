using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName ="LocaleDef",menuName ="Defs/LocaleDef" )]
public class LocaleDef : ScriptableObject
{
    /// https://docs.google.com/spreadsheets/d/e/2PACX-1vRBwcEdsIc6LYNJQIAzeB5FnoV6mayU7cLxgP-hnulsID_b2lNu_61k1F3jxDJop0mP57M3gI097Dky/pub?gid=0&single=true&output=tsv = ENG
    /// https://docs.google.com/spreadsheets/d/e/2PACX-1vRBwcEdsIc6LYNJQIAzeB5FnoV6mayU7cLxgP-hnulsID_b2lNu_61k1F3jxDJop0mP57M3gI097Dky/pub?gid=958663841&single=true&output=tsv = RUS
    /// https://docs.google.com/spreadsheets/d/e/2PACX-1vRBwcEdsIc6LYNJQIAzeB5FnoV6mayU7cLxgP-hnulsID_b2lNu_61k1F3jxDJop0mP57M3gI097Dky/pub?gid=656067981&single=true&output=tsv = ESP
    [SerializeField] private string url;
    [SerializeField] public List<LocaleItem> localeItems;
    private UnityWebRequest request;


    public Dictionary<string,string> GetData()
    {
        Dictionary<string, string> Dic = new Dictionary<string, string>();
        foreach (var item in localeItems)
        {
            Dic.Add(item.Key,item.Value);
        }
        return Dic;
    }

    public string GetValue(string key)
    {
        foreach (var item in localeItems)
        {
            if (item.Key==key)
            {
                return item.Value;
            }
            
        }
        return default;
    }
    [ContextMenu("Update Locale")]
    public void UpdateLocale()
    {
        if (request != null) return;

        request = UnityWebRequest.Get(url);
        request.SendWebRequest().completed += OnDataLoaded;
    }
    private void OnDataLoaded(AsyncOperation operation)
    {
        if (operation.isDone)
        {
            List<LocaleItem> itemList=new List<LocaleItem>();
            var rows = request.downloadHandler.text.Split('\n');
            foreach (var row in rows)
            {
                AddItem(row, itemList);
            }
            localeItems = itemList;
            request = null;
        }
    }
    private void AddItem(string row,List<LocaleItem> list)
    {
        try
        {
            var parts = row.Split('\t');
            list.Add(new LocaleItem { Key = parts[0], Value = parts[1] });
        }
        catch (Exception e)
        {
            Debug.LogError($"Can't parse row : {row}.\n {e}");
        }

    }

}

[Serializable]
public class LocaleItem
{
    public string Key;
    public string Value;
}
