using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    [SerializeField]
    private Image _fadeImage;
    private SettingPanel _settingPanel;

    private void Start()
    {
        AudioManager.Inst.PlayMusic("main_screen");
        _settingPanel = FindObjectOfType<SettingPanel>(true).GetComponent<SettingPanel>();
    }

    public void PlayGame()
    {
        _fadeImage.raycastTarget = true;
        AudioManager.Inst.PlaySFX("mouse_click");
        
        AudioManager.Inst.FadeOutMusic("main_screen");
        _fadeImage.DOFade(1f, 2f)
            .OnKill(() =>
            {
                SceneManager.LoadScene("Intro");
            });
    }

    public void OnClickSettingButton()
    {
        PopUpUIManager.Inst.OpenUI(_settingPanel);
    }

    public void QuitGame()
    {
        AudioManager.Inst.PlaySFX("mouse_click");
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
