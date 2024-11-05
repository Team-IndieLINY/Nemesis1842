using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeSlider : MonoBehaviour
{
    private Slider _slider;

    [SerializeField]
    private TextMeshProUGUI _soundVolumeText;
    // Start is called before the first frame update
    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void UpdateSoundVolumeText()
    {
        int soundVolume = (int)_slider.value;
        _soundVolumeText.text = soundVolume.ToString();
    }

    public void SetMasterVolume()
    {
        int volume = int.Parse(_soundVolumeText.text);
        volume = ConvertAudioMixerVolume(volume);
        
        AudioManager.Inst.SetAllSoundVolume(volume);
    }

    public void SetBGMVolume()
    {
        int volume = int.Parse(_soundVolumeText.text);
        volume = ConvertAudioMixerVolume(volume);
        
        AudioManager.Inst.SetBackgroundSoundVolume(volume);
    }
    public void SetSFXVolume()
    {
        int volume = int.Parse(_soundVolumeText.text);
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
