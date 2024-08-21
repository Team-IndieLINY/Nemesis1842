using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasteMachine : CocktailMachine
{
    public enum TasteType
    {
        Sweet,
        Sour,
        Bitter,
        Salty
    }
    
    private TasteType? _currentTasteType;
    
    public override void OnClickDecisionButton()
    {
        _cocktailMakingManager.SetTaste(_currentTasteType);
    }

    public void SetCurrentTasteType(TasteType tasteType)
    {
        _currentTasteType = tasteType;
    }
}
