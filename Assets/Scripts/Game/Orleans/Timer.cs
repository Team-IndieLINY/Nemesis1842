using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timerText;

    [SerializeField]
    private int time;

    public IEnumerator StartTimer()
    {
        DateTime dateTime = new DateTime(time * 10000000);
        _timerText.text = dateTime.ToString("mm:ss");
        
        while (dateTime.Ticks > 0)
        {
            dateTime = dateTime.AddTicks(-10000000);
            _timerText.text = dateTime.ToString("mm:ss");

            yield return new WaitForSecondsRealtime(1f);
        }
        
        gameObject.SetActive(false);
    }
}
