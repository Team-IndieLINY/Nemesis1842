using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class CocktailMachineManager : MonoBehaviour
{
    private CocktailMachine _currentCocktailMachine;
    
    [SerializeField]
    private RectTransform _cocktailMachineDownPoint;

    [SerializeField]
    private RectTransform _cocktailMachineUpPoint;
    
    [SerializeField]
    private CocktailMachine _tasteMachine;

    private void Awake()
    {
        _currentCocktailMachine = _tasteMachine;
    }

    public void ResetCocktailMachine()
    {
        _currentCocktailMachine.GetComponent<RectTransform>().anchoredPosition =
            _cocktailMachineUpPoint.anchoredPosition;

        _tasteMachine.GetComponent<RectTransform>()
                .anchoredPosition =
            _cocktailMachineDownPoint.anchoredPosition;
        
        _currentCocktailMachine.InActivateCocktailMachine();
        _tasteMachine.ActivateCocktailMachine();
        _currentCocktailMachine = _tasteMachine;
    }

    public void AnimateChangingCocktailMachine(CocktailMachine cocktailMachine)
    {
        if (_currentCocktailMachine == cocktailMachine)
        {
            return;
        }
        
        Sequence sequence = DOTween.Sequence();

        sequence
            .Append(_currentCocktailMachine.GetComponent<RectTransform>()
                .DOAnchorPos(_cocktailMachineUpPoint.anchoredPosition, 0.5f).SetEase(Ease.InBack))
            .Append(cocktailMachine.GetComponent<RectTransform>()
                .DOAnchorPos(_cocktailMachineDownPoint.anchoredPosition, 0.3f).SetEase(Ease.OutBack))
            .OnKill(() =>
            {
                _currentCocktailMachine = cocktailMachine;
                cocktailMachine.ActivateCocktailMachine();
            });
    }
}