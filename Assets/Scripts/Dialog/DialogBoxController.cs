using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogBoxController : MonoBehaviour
{
    [SerializeField] private DialogContainer DialogBox_1;
    [SerializeField] private DialogContainer DialogBox_2;
    [SerializeField] private float typeDelay;
    [SerializeField] private AudioClip typeSound;
    [SerializeField] private AudioClip closeSound;
    [SerializeField] private AudioClip openSound;
    private AudioSource source;
    private DialogContainer currentBox;
    private DialogData dialogData;
    private int index;
    private Coroutine typingRoutine;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    private PlayerInput playerInput;
    private GeneralUIController controller;
    private void Start()
    {
        controller = FindObjectOfType<GeneralUIController>();
       source =GameObject.FindWithTag("SFX").GetComponent<AudioSource>();
    }
    public void StartDialog(DialogData _data)
    {
        controller.locker.Retain(this);
        controller.InterfaceLocker.Retain(this);
        FindPlayerInput();
        playerInput.enabled = false;
        if (typingRoutine != null) return;

        index = 0;
        dialogData = _data;
        OpenDialogBox();
        
    }
    public void FindPlayerInput()
    {
        if (playerInput==null)
        {
        playerInput = FindObjectOfType<Hero>().GetComponent<PlayerInput>();
        }
    }
    private void SetCurrentBox(DialogBox box)
    {
        switch (box)
        {
            case DialogBox.DialogBox_1:
                currentBox = DialogBox_1;
                break;
            case DialogBox.DialogBox_2:
                currentBox = DialogBox_2;
                break;
            default:throw new Exception("There is no Current Box");
        }
    }
    [ContextMenu("StartType")]
     public void StartTypingRoutine()
    {
        typingRoutine=StartCoroutine(TypingRoutine());
    }
    private IEnumerator TypingRoutine()
    {
        currentBox.Text.text = string.Empty;
        var sentence = dialogData.sentences[index];
        currentBox.Icon.sprite = sentence.Image;
        foreach (var letter in sentence.Text)
        {
            currentBox.Text.text += letter;
            source.PlayOneShot(typeSound);
        yield return new WaitForSeconds(typeDelay);
        }
        typingRoutine = null;
    }
    public void Skip()
    {
        if (typingRoutine == null) return;
        StopCoroutine(typingRoutine);
        typingRoutine = null;
        currentBox.Text.text = dialogData.sentences[index].Text;
    }
    public void NextSentence()
    {
        index++;
        if (typingRoutine != null)
        {
            StopCoroutine(typingRoutine);
        }
        if (index>=dialogData.sentences.Length)
        {
            CloseDialogBox();
            EndOfDialog();
        }
        else
        if(dialogData.sentences[index].Box!=currentBox.Enum)
        {
            CloseDialogBox();
            OpenDialogBox();
        }else
        {
            StartTypingRoutine();
        }
    }
    private void OpenDialogBox()
    {
        SetCurrentBox(dialogData.sentences[index].Box);
        currentBox.gameObject.SetActive(true);
        currentBox.animator.SetBool(IsOpen, true);
        source.PlayOneShot(openSound);
        StartTypingRoutine();
    }
    private void CloseDialogBox()
    {
        currentBox.animator.SetBool(IsOpen, false);
        currentBox.gameObject.SetActive(false);
        source.PlayOneShot(closeSound);
        typingRoutine = null;
    }
    private void EndOfDialog()
    {
        controller.locker.Dispose(this);
        controller.InterfaceLocker.Dispose(this);
        FindPlayerInput();
        playerInput.enabled = true;
        dialogData.OnDialogEnd?.Invoke();
    }
}
