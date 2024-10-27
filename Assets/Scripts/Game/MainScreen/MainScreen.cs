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
    
    public void PlayGame()
    {
        _fadeImage.DOFade(1f, 2f)
            .OnKill(() =>
            {
                SceneManager.LoadScene("Intro");
            });
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
