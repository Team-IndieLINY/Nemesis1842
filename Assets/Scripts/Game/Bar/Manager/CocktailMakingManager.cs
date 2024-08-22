using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class CocktailMakingManager : MonoBehaviour
{
    private Cocktail _cocktail = new();
    private PlayableDirector _playableDirector;
    

    [SerializeField]
    private CocktailEvaluationManager _cocktailEvaluationManager;

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
    }

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
        _cocktailEvaluationManager.EvaluateCustomerPatient();
    }
    
    public void OnClickResetButton()
    {
        _playableDirector.Play();
        _cocktail.ResetCocktail();
    }
}
