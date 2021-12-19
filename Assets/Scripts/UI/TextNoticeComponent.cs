using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextNoticeComponent : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip defaultClip;
    [SerializeField] private AudioClip closeTabClip;
    private AudioSource source;

    private static readonly int Open = Animator.StringToHash("Open");

    private void Start()
    {
        source = GameObject.FindWithTag("SFX").GetComponent<AudioSource>();
    }

    public void CallTextNotice(string value)
    {
        text.text = value;
        source.PlayOneShot(defaultClip);
        animator.SetTrigger(Open);
    }
    public void CallTextNotice(string value, AudioClip clip)
    {
        source.PlayOneShot(clip);
        animator.SetTrigger(Open);
    }
    public void OnCloseTab()
    {

    }
    [ContextMenu("TestFunc")]
    public void Test()
    {
        CallTextNotice("Test");
    }
}
