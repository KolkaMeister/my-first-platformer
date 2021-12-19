using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager 
{
    public static readonly LocalizationManager I;

    private StringPersistentProperty localKey = new StringPersistentProperty("ENG", "currentLocale");

    public StringPersistentProperty LocaleKey => localKey;

    public static string[] LocaleNames = new string[] { "ENG","RUS","ESP","FRN"};

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
        localKey.Value = key;
        localeDef = Resources.Load<LocaleDef>($"Locales/{localKey.Value}");
        if (localeDef == null)
        {
            localKey.Value = localKey.defaultValue;
            localeDef = Resources.Load<LocaleDef>($"Locales/{localKey.Value}");
            localeDic = localeDef.GetData();
            OnLocaleLoaded?.Invoke();

        }
        else
        {
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
