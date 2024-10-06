using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CocktailMakingManager : MonoBehaviour
{
    private Cocktail _cocktail = new();
    public Cocktail Cocktail => _cocktail;
    

    // [SerializeField]
    // private CocktailEvaluationManager _cocktailEvaluationManager;

    [SerializeField]
    private CocktailManager _cocktailManager;

    [SerializeField]
    private ShakerInfoUI _shakerInfoUI;

    private void Awake()
    {
    }

    public void SetTaste(Cocktail.ETasteType? tasteType)
    {
        _cocktail.SetTaste(tasteType);
        _shakerInfoUI.UpdateShakerInfoUI();
    }

    public void SetScent(Cocktail.EScentType? scentType)
    {
        _cocktail.SetScent(scentType);
        _shakerInfoUI.UpdateShakerInfoUI();
    }

    public void SetAlcohol(int alcoholAmount)
    {
        Debug.Log(alcoholAmount);
        _cocktail.SetAlcohol(alcoholAmount);
    }

    public void ResetCocktail()
    {
        _cocktail.ResetCocktail();
        _shakerInfoUI.ResetShakerInfoUI();
    }
}
