using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
   public static AudioManager Inst { get; private set; }
    
    public Sound[] musicSounds;
    public Sound[] sfxSounds;
    
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioMixer audioMixer;

    private Coroutine _repeatCoroutine;
    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }

    public void StopMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);
        
        if (sound == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Stop();
        }
    }
    public void FadeInMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);
        
        if (sound == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = sound.clip;
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
            musicSource.DOFade(1f, 2f);
        }
    }
    public void FadeOutMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);
        
        if (sound == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.DOFade(0f, 2f)
                .OnComplete(() =>
                {
                    musicSource.Stop();
                });
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(sound.clip);
        }
    }
    
    public void StopRepeatEnvironmentSound()
    {
        if (_repeatCoroutine != null)
        {
            StopCoroutine(_repeatCoroutine);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }
    public void SetAllSoundVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }
    public void SetBackgroundSoundVolume(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
    }

    public void SetSFXSoundVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }
}
