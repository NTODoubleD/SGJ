using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _speechSource;

    [HideInInspector] public static AudioBehaviour Instance;
   

    private void Awake()
    {
        Instance = this;
    }

    public void PlayMusic(AudioClip newClip)
    {
        _musicSource.clip = newClip;
        _musicSource.Play();
    }

    public void PlaySpeech(AudioClip newClip)
    {
        _speechSource.clip = newClip;
        _speechSource.Play();
    }



}
