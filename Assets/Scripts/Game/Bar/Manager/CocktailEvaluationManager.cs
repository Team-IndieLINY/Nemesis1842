using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocktailEvaluationManager : MonoBehaviour
{
    private readonly Cocktail _requiringCocktail = new();

    private int _sumOfCocktailCost = 0;
    public int SumOfCocktailCost => _sumOfCocktailCost;
    
    private float _tipProbability = 40f;
    
    private int _baseCocktailCost = 50;
    public int BaseCocktailCost => _baseCocktailCost;

    private int _tipCost;
    public int TipCost => _tipCost;
    
    private int _customerPatientTime = 30;
    private int _currentCustomerPatientTime;
    
    private Coroutine _workCustomerPatientTimer;
    
    private const float BASE_TIP_PERCENTAGE_OF_COCKTAIL_COST = 0.5f;

    public void StartCoroutineWorkCustomerPatientTimer()
    {
        _workCustomerPatientTimer = StartCoroutine(WorkCustomerPatientTimer());
    }
    private IEnumerator WorkCustomerPatientTimer()
    {
        _currentCustomerPatientTime = _customerPatientTime;
        
        while (_currentCustomerPatientTime != 0)
        {
            _currentCustomerPatientTime--;

            yield return new WaitForSeconds(1f);
        }
    }
    
    public void SetRequiringCocktailData(List<BarCocktailProblemEntity> barCocktailProblemEntities)
    {
        foreach (var barCocktailProblemEntity in barCocktailProblemEntities)
        {
            if (barCocktailProblemEntity.answer_type == 0) //맛
            {
                _requiringCocktail.SetTaste((TasteMachine.TasteType)barCocktailProblemEntity.answer);
            }
            else if (barCocktailProblemEntity.answer_type == 1) // 향
            {
                _requiringCocktail.SetScent((ScentMachine.ScentType)barCocktailProblemEntity.answer);
            }
        }
    }

    public void EvaluateCocktail(Cocktail comparedCocktail)
    {
        int correctedCount = _requiringCocktail.CompareByCountingTo(comparedCocktail);
        
    }

    public void EvaluateCustomerPatient()
    {
        StopCoroutine(_workCustomerPatientTimer);

        _tipProbability = (float)_currentCustomerPatientTime / _customerPatientTime * 100f;
    }

    public void CalculateCocktailCost()
    {
        _sumOfCocktailCost = _baseCocktailCost;
        _tipCost = 0;

        int randomNumber = Random.Range(0, 100);
        
        Debug.Log(_tipProbability);
        
        if (randomNumber < (int)_tipProbability)
        {
            _tipCost = (int)(_baseCocktailCost * BASE_TIP_PERCENTAGE_OF_COCKTAIL_COST);
            _sumOfCocktailCost += _tipCost;
        }
    }
}
