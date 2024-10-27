using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    [SerializeField]
    private GameObject _enterAlcoholPhaseButtonGO;

    [SerializeField]
    private GameObject _openRecipeButtonGO;

    [SerializeField]
    private CocktailRecipeUI _cocktailRecipeUI;

    private List<CocktailData> _cocktailDatas = new();

    private Cocktail.ETasteType? _tasteType;
    public Cocktail.ETasteType? TasteType => _tasteType;
    
    private Cocktail.EScentType? _scentType;
    public Cocktail.EScentType? ScentType => _scentType;

    private CocktailData _resultCocktailData;
    public CocktailData ResultCocktailData => _resultCocktailData;

    private string _cocktailCode;
    public string CocktailCode => _cocktailCode;

    private int _cocktailMistakeCount;
    public int CocktailMistakeCount => _cocktailMistakeCount;
    
    private List<CocktailRejectScriptEntity> _rejectScriptDatas;

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

    public void SetStepCocktail(string cocktailCode, List<CocktailRejectScriptEntity> rejectScriptEntities)
    {
        _rejectScriptDatas = rejectScriptEntities;
        _cocktailCode = cocktailCode;
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

    public void ResetTurnCocktail()
    {
        _cocktailMistakeCount = 0;
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

                if (_resultCocktailData.CocktailCode != _cocktailCode)
                {
                    _cocktailMistakeCount++;
                    
                    int randomIndex = Random.Range(0, _rejectScriptDatas.Count);
                    CocktailMakingScreenDialougeManager.Inst.StartDialogue(_rejectScriptDatas[randomIndex]
                        .cockail_reject_script);
                }
                else
                {
                    _enterAlcoholPhaseButtonGO.SetActive(false);
                    _openRecipeButtonGO.SetActive(false);
                    _cocktailRecipeUI.OnClickCloseRecipeButton(_openRecipeButtonGO);
                    BarGameManager.Inst.OnClickEnterAlcoholPhaseButton();
                }
                
                return;
            }
        }

        _cocktailMistakeCount++;
                    
        int randomIndex2 = Random.Range(0, _rejectScriptDatas.Count);
        CocktailMakingScreenDialougeManager.Inst.StartDialogue(_rejectScriptDatas[randomIndex2]
            .cockail_reject_script);
    }

    private bool IsMaterialEmpty()
    {
        return TasteType == null || ScentType == null;
    }
}
