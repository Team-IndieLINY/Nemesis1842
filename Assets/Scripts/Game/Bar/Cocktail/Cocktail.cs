using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cocktail
{
    private TasteMachine.TasteType? _tasteType = null;
    public TasteMachine.TasteType? TasteType => _tasteType;
    
    private ScentMachine.ScentType? _scentType = null;
    public ScentMachine.ScentType? ScentType => _scentType;
    
    private int _alcohol = -1;
    public int Alcohol => _alcohol;

    public void SetTaste(TasteMachine.TasteType? tasteType)
    {
        _tasteType = tasteType;
    }

    public void SetScent(ScentMachine.ScentType? scentType)
    {
        _scentType = scentType;
    }

    public void SetAlcohol(int alcoholAmount)
    {
        _alcohol = alcoholAmount;
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
