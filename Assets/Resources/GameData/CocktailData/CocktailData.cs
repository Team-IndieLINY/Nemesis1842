using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CocktailData",fileName = "CocktailData",order = Int32.MaxValue)]
public class CocktailData : ScriptableObject
{
    [SerializeField]
    private string _cocktailCode;

    public string CocktailCode => _cocktailCode;
    
    [SerializeField]
    private string _cocktailName;

    public string CocktailName => _cocktailName;

    [SerializeField]
    private string _cocktailDescription;

    public string CocktailDescription => _cocktailDescription;

    [SerializeField]
    private Sprite _cocktailSprite;

    public Sprite CocktailSprite => _cocktailSprite;

    [SerializeField]
    private Cocktail.ETasteType _tasteType;

    public Cocktail.ETasteType TasteType => _tasteType;

    [SerializeField]
    private Cocktail.EScentType _scentType;

    public Cocktail.EScentType ScentType => _scentType;
}
