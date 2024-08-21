using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocktailEvaluationManager : MonoBehaviour
{
    private readonly Cocktail _requiringCocktail = new();
    
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
        Debug.Log(correctedCount);
    }
}
