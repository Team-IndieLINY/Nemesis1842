using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocktailTasteMaterial : CocktailMaterial
{
    [SerializeField]
    private Cocktail.ETasteType _tasteType;
    
    public override void SetCocktail()
    {
        _cocktailMakingManager.SetTaste(_tasteType);
        
    }
}
