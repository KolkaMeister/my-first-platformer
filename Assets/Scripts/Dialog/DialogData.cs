using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public class DialogData
{
    [SerializeField] public Sentence[] sentences;
    [SerializeField] private UnityEvent onDialogEnd;

    public UnityEvent OnDialogEnd => onDialogEnd;

    [Serializable]
    public class Sentence
    {
        [SerializeField] private Sprite image;
        [SerializeField] private DialogBox box;
        [SerializeField] private string text;

        public Sprite Image => image;
        public DialogBox Box => box;
        public string Text => text;
    }
}
