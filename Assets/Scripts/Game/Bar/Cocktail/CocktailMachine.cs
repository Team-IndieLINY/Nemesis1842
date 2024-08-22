using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public abstract class CocktailMachine : MonoBehaviour
{
    [SerializeField]
    private CocktailMachineManager _cocktailMachineManager;

    [SerializeField]
    protected CocktailMakingManager _cocktailMakingManager;
    
    [SerializeField]
    protected List<Button> _cocktailMachineButtons;

    protected bool _isUsed; //해당 칵테일 기계를 사용했는 지 여부

    public void InActivateCocktailMachine()
    {
        foreach (var cocktailMachineButton in _cocktailMachineButtons)
        {
            cocktailMachineButton.interactable = false;
        }
    }

    public void ActivateCocktailMachine()
    {
        foreach (var cocktailMachineButton in _cocktailMachineButtons)
        {
            cocktailMachineButton.interactable = true;
        }
    }

    public void OnClickCocktailMachineSelectingButton()
    {
        InActivateCocktailMachine();
        _cocktailMachineManager.AnimateChangingCocktailMachine(this);
    }

    public void ResetIsUsed()
    {
        _isUsed = false;
    }

    public abstract void OnClickDecisionButton();
}