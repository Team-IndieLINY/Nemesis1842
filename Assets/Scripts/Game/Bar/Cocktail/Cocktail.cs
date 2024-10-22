using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cocktail
{
    public enum ETasteType
    {
        JEGERMEISTER,
        KAHLUA,
        MALIBU,
        PEACHTREE
    }

    public enum EScentType
    {
        SCIENTICCINNAMON,
        BASILBLEND,
        STARDUSTSUGAR,
        METEORALMOND,
        COLDMOON,
        PINKBLUEMING
    }
    
    private ETasteType? _tasteType = null;
    public ETasteType? TasteType => _tasteType;
    
    private EScentType? _scentType = null;
    public EScentType? ScentType => _scentType;
    
    private int _alcohol = -1;
    public int Alcohol => _alcohol;

    public void SetTaste(ETasteType? tasteType)
    {
        _tasteType = tasteType;
    }

    public void SetScent(EScentType? scentType)
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
