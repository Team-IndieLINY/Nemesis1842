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

    [SerializeField]
    private GameObject _settingPanelGO;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _settingPanelGO.SetActive(false);
        }
    }

    private void Awake()
    {
        AudioManager.Inst.PlayMusic("evening");
    }

    public void PlayGame()
    {
        AudioManager.Inst.PlaySFX("mouse_click");
        _fadeImage.DOFade(1f, 2f)
            .OnKill(() =>
            {
                SceneManager.LoadScene("Intro");
            });
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
