using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocktailScentMaterial : CocktailMaterial
{
    [SerializeField]
    private Cocktail.EScentType _scentType;
    
    
    public override void SetCocktail()
    {
        _cocktailMakingManager.SetScent(_scentType);
    }
}
