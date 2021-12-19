using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : Button
{
    [SerializeField] private GameObject normal;
    [SerializeField] private GameObject selected;
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);
        normal.SetActive(SelectionState.Pressed != state);
        selected.SetActive(SelectionState.Pressed == state);
    }
}
