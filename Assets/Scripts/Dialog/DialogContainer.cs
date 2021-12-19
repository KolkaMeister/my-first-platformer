using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogContainer : MonoBehaviour
{
   
    [SerializeField] public DialogBox Enum;
    [SerializeField] public Image Icon;
    [SerializeField] public Text Text;
    [SerializeField] public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}
