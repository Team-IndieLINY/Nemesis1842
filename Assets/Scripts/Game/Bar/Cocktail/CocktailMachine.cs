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

    public abstract void OnClickDecisionButton();
}
