using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private AudioClip audioClip;
    private AudioSource source;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (source == null)
        {
            source = GameObject.FindWithTag("SFX").GetComponent<AudioSource>();

        }
        source.PlayOneShot(audioClip);
    }
}
