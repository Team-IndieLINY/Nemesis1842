using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CocktailRecipeUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform _cocktailRecipeOpenRectTransform;

    [SerializeField]
    private RectTransform _cocktailRecipeCloseRectTransform;

    [SerializeField]
    private GameObject _cocktailRecipeBlockPrefab;

    [SerializeField]
    private Transform _cocktailDataScrollViewContentTransform;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void InitializeCocktailRecipeUI(List<CocktailData> cocktailDatas)
    {
        foreach (var cocktailData in cocktailDatas)
        {
            GameObject cocktailRecipeBlockGO =
                Instantiate(_cocktailRecipeBlockPrefab, _cocktailDataScrollViewContentTransform);
            
            if(cocktailRecipeBlockGO.TryGetComponent(out CocktailRecipeBlockUI cocktailRecipeBlockUI))
            {
                cocktailRecipeBlockUI.InitializeCocktailRecipeBlockUI(cocktailData);
            }
        }
    }

    public void OnClickOpenRecipeButton(GameObject recipeOpenbuttonGO)
    {
        AudioManager.Inst.PlaySFX("recipe_open");
        recipeOpenbuttonGO.SetActive(false);
        _rectTransform.DOAnchorPos(_cocktailRecipeOpenRectTransform.anchoredPosition, 0.6f);

        if (TutorialManager.Inst.UseTutorial && TutorialManager.Inst.TutorialUIIndex == 7)
        {
            TutorialManager.Inst.ShowTutorial();
        }
    }
    public void OnClickCloseRecipeButton(GameObject recipeOpenButtonGO)
    {
        recipeOpenButtonGO.SetActive(true);
        _rectTransform.DOAnchorPos(_cocktailRecipeCloseRectTransform.anchoredPosition, 0.6f);
    }
}
