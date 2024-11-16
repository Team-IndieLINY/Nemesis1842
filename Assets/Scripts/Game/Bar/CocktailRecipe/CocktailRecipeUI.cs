using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CocktailRecipeUI : MonoBehaviour
{

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
}
