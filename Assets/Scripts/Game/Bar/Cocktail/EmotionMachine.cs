using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionMachine : CocktailMachine
{
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
    }
}
