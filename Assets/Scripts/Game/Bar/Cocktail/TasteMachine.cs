using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TasteMachine : CocktailMachine
{
    [SerializeField]
    private TasteLiquid _tasteLiquid;

    [SerializeField]
    private TextMeshProUGUI _currentTasteText;
    
    public enum TasteType
    {
        Sweet,
        Sour,
        Bitter,
        Salty
    }

    private void Awake()
    {
        _cocktailMachineSelectionLightImage.material.SetColor(
            "_EmissionColor", new Color(1f, 0f, 0f, 1f) * 20f);
    }

    private TasteType? _currentTasteType;
    
    public override void OnClickDecisionButton()
    {
        if (_currentTasteType == null)
        {
            return;
        }
        if (_isUsed is true)
        {
            return;
        }
        _isUsed = true;
        
        _tasteLiquid.AnimateStartFallingOffLiquid(_currentTasteType);
        _cocktailMakingManager.SetTaste(_currentTasteType);
    }

    public void SetCurrentTasteType(TasteType tasteType)
    {
        _currentTasteType = tasteType;
        _currentTasteText.text = tasteType.ToString().ToUpper();
    }

    public void ResetTasteMachine()
    {
        _currentTasteType = null;
        _currentTasteText.text = "";
    }
}
