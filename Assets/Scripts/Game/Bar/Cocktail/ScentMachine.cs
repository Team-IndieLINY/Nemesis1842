using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScentMachine : CocktailMachine
{
    public enum ScentType
    {
        Smoky, // 스모키
        Floral, // 꽃
        Fruity, // 과일
        Nutty, // 견과류
        Malty, //몰트
        Medicinal //약내음
    }
    
    [SerializeField]
    private ScentType _currentScentType;
    
    private void Awake()
    {
        _cocktailMachineSelectionLightImage.material.SetColor(
            "_EmissionColor", new Color(1f, 0f, 0f, 1f));
    }
    
    public override void OnClickDecisionButton()
    {
        if (_isUsed is true)
        {
            return;
        }
        _isUsed = true;
        
        _cocktailMakingManager.SetScent(_currentScentType);
    }
}
