using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AlcoholCalculator : MonoBehaviour
{
    [SerializeField]
    private ScanEvaluator _scanEvaluator;
    [SerializeField]
    private AlcoholCalculatorUI _cocktailMakingScreenAlcoholCalculatorUI;

    [SerializeField]
    private CocktailMakingManager _cocktailMakingManager;

    [SerializeField]
    private Guest _guest;
    
    private int _maxAlcohol = 100;
    public int MaxAlcohol => _maxAlcohol;
    
    private int _minAlcohol = 0;
    public int MinAlcohol => _minAlcohol;

    private int _maxAnswerAlcohol;
    private int _minAnswerAlcohol;
    
    private void Awake()
    {
        
    }

    public void SetAnswerAlcohol(int maxAnswerAlcohol, int minAnswerAlcohol)
    {
        _maxAnswerAlcohol = maxAnswerAlcohol;
        _minAnswerAlcohol = minAnswerAlcohol;
    }

    public void ApplyScanResult()
    {
        if (_scanEvaluator.IsSuccess)
        {
            _scanEvaluator.IsSuccess = false;
            _maxAlcohol -= (int)((_maxAlcohol - _maxAnswerAlcohol) * 0.4f);
            _minAlcohol += (int)((_minAnswerAlcohol - _minAlcohol) * 0.4f);
            
            UpdateAlcoholGaugeBar();
        }
    }

    public void EvaluateAlcohol()
    {
        int alcohol = _cocktailMakingManager.Cocktail.Alcohol;
        
        if (alcohol >= _minAnswerAlcohol &&
            alcohol <= _maxAnswerAlcohol)
        {
            _guest.AlcoholReactionType = Guest.EAlcoholReactionType.FIT;
        }
        else if (alcohol > _maxAnswerAlcohol)
        {
            if (_maxAlcohol > alcohol)
            {
                _maxAlcohol = alcohol;
            }
            _guest.AlcoholReactionType = Guest.EAlcoholReactionType.HIGH;
        }
        else if (alcohol < _minAnswerAlcohol)
        {
            if (_minAlcohol < alcohol)
            {
                _minAlcohol = alcohol;
            }
            _guest.AlcoholReactionType = Guest.EAlcoholReactionType.LOW;
        }
        
        UpdateAlcoholGaugeBar();
    }

    public void UpdateAlcoholGaugeBar()
    {
        _cocktailMakingScreenAlcoholCalculatorUI.UpdateAlcoholGaugeBarUI();
    }

    public void ResetAlcoholCalculator()
    {
        _maxAlcohol = 100;
        _minAlcohol = 0;
        
        _cocktailMakingScreenAlcoholCalculatorUI.UpdateAlcoholGaugeBarUI();
    }
}