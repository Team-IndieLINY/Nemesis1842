using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cocktail
{
    private TasteMachine.TasteType? _tasteType = null;
    private ScentMachine.ScentType? _scentType = null;
    private int _alcohol = -1;

    public void SetTaste(TasteMachine.TasteType? tasteType)
    {
        _tasteType = tasteType;
    }

    public void SetScent(ScentMachine.ScentType? scentType)
    {
        _scentType = scentType;
    }

    public void ResetCocktail()
    {
        _tasteType = null;
        _scentType = null;
    }

    public int CompareByCountingTo(Cocktail comparedCocktail)
    {
        Debug.Log(comparedCocktail._tasteType);
        
        int correctedCount = 0;
        
        if (comparedCocktail._tasteType != null && _tasteType != null && _tasteType == comparedCocktail._tasteType)
        {
            correctedCount++;
        }
        
        if (comparedCocktail._scentType != null && _scentType != null && _scentType == comparedCocktail._scentType)
        {
            correctedCount++;
        }
        
        return correctedCount;
    }
}
