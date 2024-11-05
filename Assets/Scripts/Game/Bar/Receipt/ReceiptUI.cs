using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReceiptUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TextMeshProUGUI _cocktailCountText;
    [SerializeField]
    private TextMeshProUGUI _cocktailSumOfPriceText;

    [SerializeField]
    private TextMeshProUGUI _overloadCountText;
    
    [SerializeField]
    private TextMeshProUGUI _overloadSumOfPriceText;

    [SerializeField]
    private TextMeshProUGUI _cocktailMistakeCountText;
    
    [SerializeField]
    private TextMeshProUGUI  _cocktailMistakeSumOfPriceText;


    [SerializeField]
    private TextMeshProUGUI _sumOfMoneyText;

    [SerializeField]
    private TextMeshProUGUI _monthlyRentText;
    
    [SerializeField]
    private TextMeshProUGUI  _earningText;
    
    [SerializeField]
    private TextMeshProUGUI  _restMonthlyRentText;

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
        int sumOfCocktailMistakePrice = -_barGameManager.CocktailMistakePrice *_cocktailMakingManager.CocktailMistakeCount;
        int sumOfOverloadPrice = -_barGameManager.OverloadPrice * _alcoholController.SumOfOverloadCount;

        //칵테일 제공
        _cocktailCountText.text = "(" + _barGameManager.StepCount + "회)";
        _cocktailSumOfPriceText.color = new Color32(46, 113, 52, 255);
        _cocktailSumOfPriceText.text = "+$" + _barGameManager.CocktailPrice * _barGameManager.StepCount;

        //제조 실패
        _cocktailMistakeCountText.text = "(" + _cocktailMakingManager.CocktailMistakeCount + "회)";
        if (sumOfCocktailMistakePrice < 0)
        {
            _cocktailMistakeSumOfPriceText.color = new Color32(162, 82, 82, 255);
            _cocktailMistakeSumOfPriceText.text = "-$" + -sumOfCocktailMistakePrice;
        }
        else
        {
            _cocktailMistakeSumOfPriceText.color = new Color32(46, 113, 52, 255);
            _cocktailMistakeSumOfPriceText.text = "+$" + sumOfCocktailMistakePrice;
        }
        
        //과부하
        _overloadCountText.text = "(" + _alcoholController.SumOfOverloadCount + "회)";
        
        if (sumOfOverloadPrice < 0)
        {
            _overloadSumOfPriceText.color = new Color32(162, 82, 82, 255);
            _overloadSumOfPriceText.text = "-$" + -sumOfOverloadPrice;
        }
        else
        {
            _overloadSumOfPriceText.color = new Color32(46, 113, 52, 255);
            _overloadSumOfPriceText.text = "+$" + sumOfOverloadPrice;
        }

        int sumOfPrice = sumOfCocktailPrice + sumOfOverloadPrice + sumOfCocktailMistakePrice;

        if (sumOfPrice < 0)
        {
            _sumOfMoneyText.color = new Color32(162, 82, 82, 255);
            _sumOfMoneyText.text = "-$" + -sumOfPrice;
            _earningText.color = new Color32(162, 82, 82, 255);
            _earningText.text = "-$" + -sumOfPrice;
        }
        else
        {
            _sumOfMoneyText.color = new Color32(46, 113, 52, 255);
            _sumOfMoneyText.text = "+$" + sumOfPrice;
            _earningText.color = new Color32(46, 113, 52, 255);
            _earningText.text = "+$" + sumOfPrice;
        }

        _monthlyRentText.text = "$" + DayManager.Instance.MonthlyRent;
        

        _restMonthlyRentText.text = "$" +
                                (_player.Money + sumOfPrice) +
                                "/" + DayManager.Instance.MonthlyRent;
    }

    public void ShowReceipt()
    {
        AudioManager.Inst.PlaySFX("receipt");
        _isShown = true;
        transform.DOMove(_receiptShowPointTransform.position, 1f);
    }

    public void HideReceipt()
    {
        transform.DOMove(_receiptSpawnPointTransform.position, 0.5f)
            .OnKill(() => { _isShown = false; });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HideReceipt();
    }
}
