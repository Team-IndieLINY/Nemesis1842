using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CocktailMakingManager : MonoBehaviour
{
    // [SerializeField]
    // private CocktailEvaluationManager _cocktailEvaluationManager;

    [SerializeField]
    private ShakerInfoUI _shakerInfoUI;

    [SerializeField]
    private Button _enterAlcoholPhaseButton;

    [SerializeField]
    private SpriteRenderer _cocktailSpriteRenderer;

    private List<CocktailData> _cocktailDatas = new();

    private Cocktail.ETasteType? _tasteType;
    public Cocktail.ETasteType? TasteType => _tasteType;
    
    private Cocktail.EScentType? _scentType;
    public Cocktail.EScentType? ScentType => _scentType;

    private CocktailData _resultCocktailData;
    public CocktailData ResultCocktailData => _resultCocktailData;

    private void Awake()
    {
        _cocktailDatas = Resources.LoadAll<CocktailData>("GameData/CocktailData").ToList();
        ResetStepCocktail();
    }

    public void SetTaste(Cocktail.ETasteType? tasteType)
    {
        _tasteType = tasteType;
        _shakerInfoUI.UpdateShakerInfoUI();
        
        if (IsMaterialEmpty() is false)
        {
            _enterAlcoholPhaseButton.interactable = true;
        }
    }

    public void SetScent(Cocktail.EScentType? scentType)
    {
        _scentType = scentType;
        _shakerInfoUI.UpdateShakerInfoUI();

        if (IsMaterialEmpty() is false)
        {
            _enterAlcoholPhaseButton.interactable = true;
        }
    }

    public void ResetStepCocktail()
    {
        _enterAlcoholPhaseButton.interactable = false;
        _tasteType = null;
        _scentType = null;
        _resultCocktailData = null;
        _shakerInfoUI.ResetShakerInfoUI();
        _cocktailSpriteRenderer.color = new Color(1, 1, 1, 0);
    }

    public void FinishMakingCocktail()
    {
        foreach (var cocktailData in _cocktailDatas)
        {
            if (cocktailData.TasteType == _tasteType
                && cocktailData.ScentType == _scentType)
            {
                _resultCocktailData = cocktailData;
                _cocktailSpriteRenderer.sprite = _resultCocktailData.CocktailSprite;

                return;
            }
        }

        //후차 이름모를 칵테일로 대체
        _cocktailSpriteRenderer.sprite = null;
    }

    private bool IsMaterialEmpty()
    {
        return TasteType == null || ScentType == null;
    }
}
