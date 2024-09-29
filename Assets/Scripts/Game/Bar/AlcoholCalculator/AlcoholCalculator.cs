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
    private AlcoholCalculatorUI _guestScreenAlcoholCalculatorUI;

    [SerializeField]
    private AlcoholCalculatorUI _cocktailMakingScreenAlcoholCalculatorUI;
    
    private int _maxAlcohol = 100;
    public int MaxAlcohol => _maxAlcohol;
    
    private int _minAlcohol = 0;
    public int MinAlcohol => _minAlcohol;
    
    private void Awake()
    {
        
    }

    public void UpdateAlcoholGaugeBar(int maxAlcohol, int minAlcohol)
    {
        _maxAlcohol = Mathf.Clamp(100 + maxAlcohol, 0, 100);
        _minAlcohol = Mathf.Clamp(minAlcohol, 0, 100);

        _guestScreenAlcoholCalculatorUI.UpdateAlcoholGaugeBarUI();
        _cocktailMakingScreenAlcoholCalculatorUI.UpdateAlcoholGaugeBarUI();
    }

    public void ResetAlcoholCalculator()
    {
        _maxAlcohol = 0;
        _minAlcohol = 0;
    }
}