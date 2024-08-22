using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Receipt : MonoBehaviour
{
    [SerializeField]
    private CocktailEvaluationManager _cocktailEvaluationManager;

    [SerializeField]
    private TextMeshProUGUI _baseCocktailCostText;

    [SerializeField]
    private TextMeshProUGUI _tipCostText;

    [SerializeField]
    private TextMeshProUGUI _sumOfCostText;

    public void SetReceiptText()
    {
        _baseCocktailCostText.text = "기본 칵테일: " + _cocktailEvaluationManager.BaseCocktailCost + "$";
        _tipCostText.text = "팁: " + _cocktailEvaluationManager.TipCost + "$";
        _sumOfCostText.text = "합계: " + _cocktailEvaluationManager.SumOfCocktailCost + "$";
    }
}
