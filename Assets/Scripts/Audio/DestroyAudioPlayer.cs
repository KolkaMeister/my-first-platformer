using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DestroyAudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip audioClip;
    [SerializeField] float delay;
    private void Start()
    {
        source.PlayOneShot(audioClip);
        Destroy(gameObject, delay);
    }

}
