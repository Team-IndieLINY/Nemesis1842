using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CocktailRecipe : MonoBehaviour
{
    [SerializeField]
    private CocktailRecipeUI _cocktailRecipeUI;
    
    private List<CocktailData> _cocktailDatas = new();
    private void Awake()
    {
        _cocktailDatas = Resources.LoadAll<CocktailData>("GameData/CocktailData").ToList();
        _cocktailRecipeUI.InitializeCocktailRecipeUI(_cocktailDatas);
    }
}
