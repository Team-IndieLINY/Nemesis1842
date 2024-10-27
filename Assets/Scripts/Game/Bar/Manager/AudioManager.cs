using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _bgmAudioSource;

    [SerializeField]
    private AudioSource _sfxAudioSoruce;
    
    [SerializeField] private AudioMixer _audioMixer;
    
    private static AudioManager instance = null;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public void FadeBGMVolume(float targetVolume)
    {
        _bgmAudioSource.DOFade(targetVolume, 1.5f);
    }

    public void PlayClip(AudioClip audioClip)
    {
        _sfxAudioSoruce.clip = audioClip;
        
        _sfxAudioSoruce.Play();
    }

    public void PlaySFX(AudioClip audioClip)
    {
        _sfxAudioSoruce.PlayOneShot(audioClip);
    }
    
    public void SetMasterVolume(float volume)
    {
        _audioMixer.SetFloat("Master", volume);
    }
 
    public void SetBGMVolume(float volume)
    {
        _audioMixer.SetFloat("BGM", volume);
    }
 
    public void SetSFXVolume(float volume)
    {
        _audioMixer.SetFloat("SFX", volume);
    }
}
