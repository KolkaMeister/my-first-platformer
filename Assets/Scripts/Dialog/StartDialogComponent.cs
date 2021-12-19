using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogComponent : MonoBehaviour
{
    [SerializeField] private DialogType type;
    [SerializeField] public DialogData dialog;
    [SerializeField] public DialogDef dialogDef;
    private GeneralUIController controller;

    public DialogData Data
    {
        get
        {
            switch (type)
            {
                case DialogType.Bound:return dialog; 
                case DialogType.External:return dialogDef.data;
                    
                    throw new System.Exception("No Current Type");
            }
            return default;
        }
    }
    public void StartDialog()
    {
        if (controller==null)
        {
            controller=FindObjectOfType<GeneralUIController>();
        }
        controller.StartDialog(Data);
    }

}
