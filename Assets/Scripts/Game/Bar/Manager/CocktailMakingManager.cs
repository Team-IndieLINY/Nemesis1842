using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocktailMakingManager : MonoBehaviour
{
    private Cocktail _cocktail = new();

    [SerializeField]
    private CocktailEvaluationManager _cocktailEvaluationManager;
    
    public void SetTaste(TasteMachine.TasteType? tasteType)
    {
        _cocktail.SetTaste(tasteType);
    }
    public void SetScent(ScentMachine.ScentType? scentType)
    {
        _cocktail.SetScent(scentType);
    }
    public void SetAlcohol(int alcohol)
    {
        
    }
    public void SetEmotion()
    {
        
    }

    public void OnClickDoneButton()
    {
        _cocktailEvaluationManager.EvaluateCocktail(_cocktail);
    }
    
    public void OnClickResetButton()
    {
        _cocktail.ResetCocktail();
    }
}
