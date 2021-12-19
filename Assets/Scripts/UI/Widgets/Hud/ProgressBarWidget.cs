using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ProgressBarWidget : MonoBehaviour
{
    [SerializeField] Image progressBar;

    public void SetProgress(float value)
    {
        progressBar.fillAmount = value;
    }
}
