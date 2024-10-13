using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AlcoholController : MonoBehaviour
{
    [SerializeField]
    private BarGameManager _barGameManager;
    
    [SerializeField]
    private AlcoholControllerUI _alcoholControllerUI;
    
    private int _maxAlcohol = 100;
    public int MaxAlcohol => _maxAlcohol;
    
    private int _minAlcohol = 0;
    public int MinAlcohol => _minAlcohol;

    private int _maxAnswerAlcohol;
    public int MaxAnswerAlcohol => _maxAnswerAlcohol;
    
    private int _minAnswerAlcohol;
    public int MinAnswerAlcohol => _minAnswerAlcohol;
    
    private int _currentInputAlcohol = -1;
    public int CurrentInputAlcohol => _currentInputAlcohol;

    private int _currentAttemptCount = 0;
    public int CurrentAttemptCount => _currentAttemptCount;

    public void SetAnswerAlcohol(int maxAnswerAlcohol, int minAnswerAlcohol)
    {
        _maxAnswerAlcohol = maxAnswerAlcohol;
        _minAnswerAlcohol = minAnswerAlcohol;
    }

    public void ApplyScanResult()
    {
        
    }

    public void ResetAlcoholController()
    {
        _maxAlcohol = 100;
        _minAlcohol = 0;
        _currentInputAlcohol = -1;
        _currentAttemptCount = 0;
        
        _alcoholControllerUI.UpdateAlcoholControllerUI();
    }

    public void OnClickEnterButton()
    {
        if (_currentInputAlcohol == -1)
        {
            return;
        }
        
        _currentAttemptCount++;
        
        if (_currentInputAlcohol <= _maxAnswerAlcohol && _currentInputAlcohol >= _minAnswerAlcohol)
        {
            _alcoholControllerUI.UpdateAlcoholControllerUICoroutine();
            _barGameManager.ExitCocktailMakingScreen();
        }
        else
        {
            if (_currentInputAlcohol > _minAlcohol && _currentInputAlcohol < _minAnswerAlcohol)
            {
                _minAlcohol = _currentInputAlcohol;
            }
            else if(_currentInputAlcohol < _maxAlcohol && _currentInputAlcohol > _maxAnswerAlcohol)
            {
                _maxAlcohol = _currentInputAlcohol;
            }

            _currentInputAlcohol = -1;
            
            _alcoholControllerUI.UpdateAlcoholControllerUICoroutine();
        }
    }

    public void OnClickCancelButton()
    {
        _currentInputAlcohol = -1;
        _alcoholControllerUI.UpdateAlcoholControllerUI();
    }

    public void SetCurrentInputAlcohol(int keypadNum)
    {
        if (_currentInputAlcohol < 0)
        {
            _currentInputAlcohol = keypadNum;
        }
        else if (_currentInputAlcohol < 10)
        {
            _currentInputAlcohol *= 10;
            _currentInputAlcohol += keypadNum;
        }
        
        _alcoholControllerUI.UpdateAlcoholControllerUI();
    }
}