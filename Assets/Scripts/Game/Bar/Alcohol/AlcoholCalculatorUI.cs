using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlcoholCalculatorUI : MonoBehaviour
{
    [SerializeField]
    private AlcoholCalculator _alcoholCalculator;
    
    [SerializeField]
    private TextMeshProUGUI _maxAlcoholText;

    [SerializeField]
    private TextMeshProUGUI _minAlcoholText;
    
    [SerializeField]
    private RectTransform _alcoholGaugeBarBackgroundRectTransform;
    
    [SerializeField]
    private RectTransform _alcoholGaugeBarRectTransform;
    
    private float _oneAlcoholPerGaugeLength;
    
    public void UpdateAlcoholGaugeBarUI()
    {
        _oneAlcoholPerGaugeLength = _alcoholGaugeBarBackgroundRectTransform.sizeDelta.y * 0.01f;
        
        _maxAlcoholText.text = _alcoholCalculator.MaxAlcohol.ToString();
        _minAlcoholText.text = _alcoholCalculator.MinAlcohol.ToString();

        _alcoholGaugeBarRectTransform.offsetMax = 
            new Vector2(0, -(100 - _alcoholCalculator.MaxAlcohol) * _oneAlcoholPerGaugeLength);
        _alcoholGaugeBarRectTransform.offsetMin = 
            new Vector2(0, _alcoholCalculator.MinAlcohol * _oneAlcoholPerGaugeLength);
    }
}
