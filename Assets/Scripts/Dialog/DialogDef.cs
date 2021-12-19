using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable][CreateAssetMenu(fileName ="DialogDef",menuName = "Defs/DialogDef")]
public class DialogDef : ScriptableObject
{
    [SerializeField] public DialogData data;
}
