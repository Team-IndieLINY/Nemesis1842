using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlcoholMachine : CocktailMachine
{
    [SerializeField]
    private TextMeshProUGUI _currentAlcoholDisplayText;
    
    private int _currentAlcohol;

    private void Awake()
    {
        _currentAlcoholDisplayText.text = _currentAlcohol.ToString();
    }

    public void SetCurrentAlcohol(int amount)
    {
        _currentAlcohol = Mathf.Clamp(_currentAlcohol + amount, 0, 100);

        _currentAlcoholDisplayText.text = _currentAlcohol.ToString();
    }
    
    public override void OnClickDecisionButton()
    {
        
        _cocktailMakingManager.SetAlcohol(_currentAlcohol);
    }
}