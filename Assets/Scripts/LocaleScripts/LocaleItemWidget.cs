using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocaleItemWidget : MonoBehaviour,IItemRenderer<string>
{
    [SerializeField] private GameObject selector;
    [SerializeField] private Text text;
    [SerializeField] private string key;

    private void Start()
    {
        LocalizationManager.I.OnLocaleLoaded += CheckLocale;
    }

    public void SetData(string _key, int index)
    {
        key = _key;
        text.text = key;
        CheckLocale();
    }
    private void CheckLocale()
    {
        selector.SetActive(LocalizationManager.I.LocaleKey.Value == key);
    }

    public void SetLocalixation()
    {
        LocalizationManager.I.LoadLocale(key);
    }
    private void OnDestroy()
    {
        LocalizationManager.I.OnLocaleLoaded -= CheckLocale;
    }


}
