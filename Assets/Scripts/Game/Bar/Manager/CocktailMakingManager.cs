using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

[RequireComponent(typeof(PlayableDirector))]
public class CocktailMakingManager : MonoBehaviour
{
    private Cocktail _cocktail = new();
    public Cocktail Cocktail => _cocktail;
    private PlayableDirector _playableDirector;
    

    // [SerializeField]
    // private CocktailEvaluationManager _cocktailEvaluationManager;

    [SerializeField]
    private CocktailManager _cocktailManager;

    [SerializeField]
    private Button _doneButton;

    [SerializeField]
    private Button _resetButton;

    [SerializeField]
    private RectTransform _shakerRectTransform;

    [SerializeField]
    private RectTransform _shakerResetRectTransform;

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

    public void SetAlcohol(int alcoholAmount)
    {
        Debug.Log(alcoholAmount);
        _cocktail.SetAlcohol(alcoholAmount);
    }
    public void SetEmotion()
    {
        
    }

    public void ResetCocktail()
    {
        _cocktail.ResetCocktail();
    }

    public void OnClickDoneButton()
    {
        InActivateDoneAndResetButton();
        _cocktailManager.SetCocktailSprite(_cocktail.TasteType);
    }
    
    public void OnClickResetButton()
    {
        InActivateDoneAndResetButton();
        _playableDirector.Play();
        _cocktail.ResetCocktail();
    }

    public void InActivateDoneAndResetButton()
    {
        _doneButton.interactable = false;
        _resetButton.interactable = false;
    }
    
    public void ActivateDoneAndResetButton()
    {
        _doneButton.interactable = true;
        _resetButton.interactable = true;
    }

    public void ResetShakerPosition()
    {
        _shakerRectTransform.anchoredPosition = _shakerResetRectTransform.anchoredPosition;
    }
}
