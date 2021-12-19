using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayComponent : MonoBehaviour
{
    [SerializeField] AudioSource source;
    
    [SerializeField] float volume=1f;
    [SerializeField] SoundClip[] clips;

    private void Start()
    {
        if (source==null)
        {
            source = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        }
    }
    public void Play(string id)
    {
        foreach (var clip in clips)
        {
            if (clip.Id != id) continue;
            source.PlayOneShot(clip.AudioClip);
        }
    }
    public void PlayAtPoint(string id)
    {
        foreach (var clip in clips)
        {
            if (clip.Id != id) continue;
            AudioSource.PlayClipAtPoint(clip.AudioClip, transform.position, volume);
        }
    }



    [Serializable]
    public class SoundClip
    {
        [SerializeField] private string id;
        [SerializeField] private AudioClip audioClip;
        public string Id => id;
        public AudioClip AudioClip => audioClip;
    }
}
