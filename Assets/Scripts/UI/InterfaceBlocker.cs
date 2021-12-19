using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceBlocker : MonoBehaviour
{
    private GeneralUIController controller;

    private void Start()
    {
        controller= FindObjectOfType<GeneralUIController>();
        controller.InterfaceLocker.onChange += OnChange;
        OnChange(controller.InterfaceLocker.IsLocked);
    }
    public void OnChange(bool _value)
    {
        gameObject.SetActive(!_value);
    }
    public void OnDestroy()
    {
        controller.InterfaceLocker.onChange -= OnChange;
    }
}
