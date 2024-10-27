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
    private TextMeshProUGUI _sumOfMoneyText;

    [SerializeField]
    private Transform _receiptSpawnPointTransform;

    [SerializeField]
    private Transform _receiptShowPointTransform;

    [SerializeField]
    private AlcoholController _alcoholController;

    [SerializeField]
    private BarGameManager _barGameManager;

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
        
        _cocktailCountText.text = "칵테일 제공 횟수 X " + _barGameManager.StepCount + " = " +
                                  sumOfCocktailPrice + "$";

        _overloadCountText.text = "기계 과부하 횟수 X " + _alcoholController.SumOfOverloadCount + " = " +
                                  sumOfOverloadPrice + "$";

        _sumOfMoneyText.text = "합계: " + (sumOfCocktailPrice - sumOfOverloadPrice) + "$";
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
