using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ReceiptUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _cocktailCountText;

    [SerializeField]
    private TextMeshProUGUI _overloadCountText;

    [SerializeField]
    private TextMeshProUGUI _cocktailMistakeCountText;

    [SerializeField]
    private TextMeshProUGUI _sumOfMoneyText;

    [SerializeField]
    private TextMeshProUGUI _monthlyRentText;

    [SerializeField]
    private Transform _receiptSpawnPointTransform;

    [SerializeField]
    private Transform _receiptShowPointTransform;

    [SerializeField]
    private AlcoholController _alcoholController;

    [SerializeField]
    private BarGameManager _barGameManager;

    [SerializeField]
    private CocktailMakingManager _cocktailMakingManager;

    [SerializeField]
    private Player _player;

    private bool _isShown;
    public bool IsShown => _isShown;

    private void Awake()
    {
        transform.position = _receiptSpawnPointTransform.position;
    }

    public void UpdateReceiptUI()
    {
        int sumOfCocktailPrice = _barGameManager.CocktailPrice * _barGameManager.StepCount;
        int sumOfOverloadPrice = _barGameManager.OverloadPrice * _alcoholController.SumOfOverloadCount;
        int sumOfCocktailMistakePrice = _barGameManager.CocktailMistakePrice *_cocktailMakingManager.CocktailMistakeCount;
        
        _cocktailCountText.text = "칵테일 제공 횟수 X " + _barGameManager.StepCount + " = " +
                                  sumOfCocktailPrice + "$";

        _overloadCountText.text = "기계 과부하 횟수 X " + _alcoholController.SumOfOverloadCount + " = " +
                                  -sumOfOverloadPrice + "$";

        _cocktailMistakeCountText.text = "칵테일 실수 횟수 X " + _cocktailMakingManager.CocktailMistakeCount + " = " +
                                         -sumOfCocktailMistakePrice + "$";

        _sumOfMoneyText.text = "합계: " + (sumOfCocktailPrice - sumOfOverloadPrice - sumOfCocktailMistakePrice) + "$";

        _monthlyRentText.text = "남은 월세: " +
                                (_player.Money + sumOfCocktailPrice - sumOfOverloadPrice - sumOfCocktailMistakePrice) +
                                "/" + DayManager.Instance.MonthlyRent + "$";
    }

    public void ShowReceipt()
    {
        _isShown = true;
        transform.DOMove(_receiptShowPointTransform.position, 0.3f);
    }

    public void HideReceipt()
    {
        transform.DOMove(_receiptSpawnPointTransform.position, 0.3f)
            .OnKill(() =>
            {
                _isShown = false;
            });
    } 
}
