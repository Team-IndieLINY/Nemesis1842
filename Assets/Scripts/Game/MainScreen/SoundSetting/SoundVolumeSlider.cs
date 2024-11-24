using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeSlider : MonoBehaviour
{
    public enum ESoundType
    {
        Master,
        BGM,
        SFX
    }
    private Slider _slider;

    [SerializeField]
    private TextMeshProUGUI _soundVolumeText;

    [SerializeField]
    private ESoundType _soundType;

    private static int _masterVolume = 100;
    private static int _bgmVolume = 100;
    private static int _sfxVolume = 100;
    
    // Start is called before the first frame update
    private void Awake()
    {
        _slider = GetComponent<Slider>();
        
        switch (_soundType)
        {
            case ESoundType.Master:
                _slider.value = _masterVolume;
                break;
            case ESoundType.BGM:
                _slider.value = _bgmVolume;
                break;
            case ESoundType.SFX:
                _slider.value = _sfxVolume;
                break;
        }
    }

    private void Start()
    {
        UpdateSoundVolumeText();
    }

    public void UpdateSoundVolumeText()
    {
        AudioManager.Inst.PlaySFX("volume_slider");
        int soundVolume = (int)_slider.value;
        _soundVolumeText.text = soundVolume.ToString();

        switch (_soundType)
        {
            case ESoundType.Master:
                SetMasterVolume();
                break;
            case ESoundType.BGM:
                SetBGMVolume();
                break;
            case ESoundType.SFX:
                SetSFXVolume();
                break;
        }
    }

    private void SetMasterVolume()
    {
        int volume = int.Parse(_soundVolumeText.text);
        _masterVolume = volume;
        volume = ConvertAudioMixerVolume(volume);
        
        AudioManager.Inst.SetAllSoundVolume(volume);
    }

    private void SetBGMVolume()
    {
        int volume = int.Parse(_soundVolumeText.text);
        _bgmVolume = volume;
        volume = ConvertAudioMixerVolume(volume);
        
        AudioManager.Inst.SetBackgroundSoundVolume(volume);
    }
    private void SetSFXVolume()
    {
        int volume = int.Parse(_soundVolumeText.text);
        _sfxVolume = volume;
        volume = ConvertAudioMixerVolume(volume);
        
        AudioManager.Inst.SetSFXSoundVolume(volume);
    }
    
    private int ConvertAudioMixerVolume(float volume)
    {
        int result = (int)(volume * (40f / 100f) - 40);

        if (result <= -40)
        {
            result = -80;
        }

        return result;
    }
}
