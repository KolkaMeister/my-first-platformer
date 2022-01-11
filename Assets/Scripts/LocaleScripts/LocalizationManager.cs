using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager 
{
    public static readonly LocalizationManager I;

    private StringPersistentProperty localKey = new StringPersistentProperty("ENG", "currentLocale");

    public StringPersistentProperty LocaleKey => localKey;

    private static List<string> localeNames = new List<string> { "ENG","RUS","ESP","FRN"};

    public static List<string> LocaleNames => localeNames;

    private LocaleDef localeDef;

    private Dictionary<string, string> localeDic;

    public event Action OnLocaleLoaded;
    static LocalizationManager()
    {
        I = new LocalizationManager();
    }
    public LocalizationManager()
    {
        LoadLocale(localKey.Value);
    }
    public void LoadLocale(string key)
    {
        if (LocaleNames.Contains(key))
        {
            LocaleKey.Value = key;
            localeDef = Resources.Load<LocaleDef>($"Locales/{localKey.Value}");
            localeDic = localeDef.GetData();
            OnLocaleLoaded?.Invoke();
        }
    }
    
    public string GetByKey(string key)
    {
        return localeDic.TryGetValue(key, out var value)?value:$"#{key}#";
    }


}
