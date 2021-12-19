using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SelectionWindow : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private SelectionItemWindget itemWindget;
    private PlayerInput Input;
    private DataGroup<SelectionData, SelectionItemWindget> dataGroup;

    private Animator animator;

    static private readonly int IsOpened = Animator.StringToHash("IsOpened");

    private GeneralUIController controller;

    private void Awake()
    {
        controller = FindObjectOfType<GeneralUIController>();
        animator = GetComponent<Animator>();
        dataGroup = new DataGroup<SelectionData, SelectionItemWindget>(container, itemWindget);
    }
    public void OpenWindow(SelectionData[]  data)
    {
        controller.locker.Retain(this);
        controller.InterfaceLocker.Retain(this);
        FindPlayerInput();
        Input.enabled = false;
        animator.SetBool(IsOpened,true);
        dataGroup.SetData(data);
    }
    public void Close()
    {
        animator.SetBool(IsOpened,false);
        Input.enabled = true;
        controller.locker.Dispose(this);
        controller.InterfaceLocker.Dispose(this);
    }
    public void FindPlayerInput()
    {
        if (Input == null) Input = FindObjectOfType<Hero>().GetComponent<PlayerInput>();

    }
}
