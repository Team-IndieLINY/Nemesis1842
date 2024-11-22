using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timerText;

    [SerializeField]
    private int time;

    [SerializeField]
    private Image _fadeImage;

    [SerializeField]
    private Image _timerFill;

    public IEnumerator StartTimer()
    {
        DateTime dateTime = new DateTime(time * 10000000);
        _timerText.text = dateTime.ToString("mm:ss");
        
        while (_timerFill.fillAmount > 0)
        {
            _timerFill.fillAmount -= Time.deltaTime / time;

            if (_timerFill.fillAmount > 0)
            {
                dateTime = dateTime.AddTicks((long)(-10000000 * Time.deltaTime));
            }

            _timerText.text = dateTime.ToString("mm:ss");
            
            yield return new WaitForSeconds(Time.deltaTime);
        }
        
        AudioManager.Inst.FadeOutMusic("dawn");
        
        EndingManager.Inst.SetEndingType(EndingManager.EEndingType.BanishEnding);
        _fadeImage.DOFade(1f, 1.5f)
            .OnKill(() =>
            {
                SceneManager.LoadScene("DayEndingScene");
            });
        
        gameObject.SetActive(false);
    }
}
