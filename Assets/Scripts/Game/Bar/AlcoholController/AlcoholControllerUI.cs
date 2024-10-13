using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class AlcoholControllerUI : MonoBehaviour
{
    [SerializeField]
    private AlcoholController _alcoholController;
    
    [SerializeField]
    private TextMeshProUGUI _maxAlcoholText;

    [SerializeField]
    private TextMeshProUGUI _minAlcoholText;
    
    [SerializeField]
    private RectTransform _alcoholGaugeBarBackgroundRectTransform;
    
    [SerializeField]
    private RectTransform _alcoholGaugeBarRectTransform;

    [SerializeField]
    private TextMeshProUGUI _guessAlcoholText;

    [SerializeField]
    private TextMeshProUGUI _attemptCountText;
    
    private float _oneAlcoholPerGaugeLength;

    private void Awake()
    {
        _oneAlcoholPerGaugeLength = _alcoholGaugeBarBackgroundRectTransform.sizeDelta.x * 0.01f;
    }

    public void UpdateAlcoholControllerUI()
    {
        UpdateGuessAlcoholUI();
        UpdateAttemptCountUI();
        
        _alcoholGaugeBarRectTransform.offsetMax = new Vector2(-(100 - _alcoholController.MaxAlcohol) * _oneAlcoholPerGaugeLength, 0);
        _alcoholGaugeBarRectTransform.offsetMin = new Vector2(_alcoholController.MinAlcohol * _oneAlcoholPerGaugeLength, 0);

        _maxAlcoholText.text = _alcoholController.MaxAlcohol.ToString();
        _minAlcoholText.text = _alcoholController.MinAlcohol.ToString();
    }
    public void UpdateAlcoholControllerUICoroutine()
    {
        UpdateGuessAlcoholUI();
        UpdateAlcoholRangeUI();
        UpdateAttemptCountUI();
    }

    private void UpdateGuessAlcoholUI()
    {
        if (_alcoholController.CurrentInputAlcohol == -1)
        {
            _guessAlcoholText.text = "?";
        }
        else
        {
            _guessAlcoholText.text = _alcoholController.CurrentInputAlcohol.ToString();
        }
    }

    private void UpdateAlcoholRangeUI()
    {
        //Update Range Text
        StartCoroutine(ChangeAlcoholRangeTextCoroutine());
        
        //Update Gauge Bar
        StartCoroutine(ChangeAlcoholMaxGaugeBarCoroutine());
        StartCoroutine(ChangeAlcoholMinGaugeBarCoroutine());
    }

    private IEnumerator ChangeAlcoholRangeTextCoroutine()
    {
        int maxAlcohol = Int32.Parse(_maxAlcoholText.text);
        int minAlcohol = Int32.Parse(_minAlcoholText.text);

        while (maxAlcohol > _alcoholController.MaxAlcohol || minAlcohol < _alcoholController.MinAlcohol)
        {

            if (maxAlcohol != _alcoholController.MaxAlcohol)
            {
                maxAlcohol--;
                _maxAlcoholText.text = maxAlcohol.ToString();
            }

            if(minAlcohol != _alcoholController.MinAlcohol)
            {
                minAlcohol++;
                _minAlcoholText.text = minAlcohol.ToString();
            }

            yield return new WaitForSeconds(0.02f);
        }
    }
    
    private IEnumerator ChangeAlcoholMaxGaugeBarCoroutine()
    {
        Vector2 alcoholGaugeBarMaxOffset =
            new Vector2(-(100 - _alcoholController.MaxAlcohol) * _oneAlcoholPerGaugeLength, 0);

        while (_alcoholGaugeBarRectTransform.offsetMax.x > alcoholGaugeBarMaxOffset.x)
        {
            if (_alcoholGaugeBarRectTransform.offsetMax.x > alcoholGaugeBarMaxOffset.x)
            {
                _alcoholGaugeBarRectTransform.offsetMax =
                    new Vector2(_alcoholGaugeBarRectTransform.offsetMax.x - _oneAlcoholPerGaugeLength, 0);
            }

            yield return new WaitForSeconds(0.02f);
        }
    }

    private IEnumerator ChangeAlcoholMinGaugeBarCoroutine()
    {
        Vector2 alcoholGaugeBarMinOffset =
            new Vector2(_alcoholController.MinAlcohol * _oneAlcoholPerGaugeLength, 0);

        while (_alcoholGaugeBarRectTransform.offsetMin.x < alcoholGaugeBarMinOffset.x)
        {
            if (_alcoholGaugeBarRectTransform.offsetMin.x < alcoholGaugeBarMinOffset.x)
            {
                _alcoholGaugeBarRectTransform.offsetMin =
                    new Vector2(_alcoholGaugeBarRectTransform.offsetMin.x + _oneAlcoholPerGaugeLength, 0);
            }

            yield return new WaitForSeconds(0.02f);
        }
    }
    
    private void UpdateAttemptCountUI()
    {
        _attemptCountText.text = _alcoholController.CurrentAttemptCount + " Turn";
    }
}
