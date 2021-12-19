using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizeText : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private string key;
    [SerializeField] private string addition;

    private void Start()
    {
        CheckLoale();
        LocalizationManager.I.OnLocaleLoaded += CheckLoale;
    }
    private void CheckLoale()
    {
        text.text = LocalizationManager.I.GetByKey(key).ToUpper()+addition;
    }
    private void OnDestroy()
    {
        LocalizationManager.I.OnLocaleLoaded -= CheckLoale;
    }
}
