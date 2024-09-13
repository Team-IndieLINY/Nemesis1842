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

    [SerializeField]
    protected Image _cocktailMachineSelectionLightImage;

    [SerializeField]
    private float _turningOnSelectionLightTime = 0.1f;
    
    [SerializeField]
    private float _turningOffSelectionLightTime = 0.1f;

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
        _cocktailMachineManager.AnimateChangingCocktailMachine(this);
    }

    public void AnimateTurningOnSelectionLight()
    {
        StartCoroutine(TurningOnSelectionLightCoroutine());
    }

    private IEnumerator TurningOnSelectionLightCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _turningOnSelectionLightTime)
        {
            float intensity = Mathf.Lerp(0f, 20f, elapsedTime / _turningOnSelectionLightTime);
            
            _cocktailMachineSelectionLightImage.material.SetColor(
                "_EmissionColor", new Color(1f, 0f, 0f, 1f) * intensity);
            
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
    
    public void AnimateTurningOffSelectionLight()
    {
        StartCoroutine(TurningOffSelectionLightCoroutine());
    }
    
    private IEnumerator TurningOffSelectionLightCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _turningOffSelectionLightTime)
        {
            float intensity = Mathf.Lerp(20f, 0f, elapsedTime / _turningOffSelectionLightTime);
            
            _cocktailMachineSelectionLightImage.material.SetColor(
                "_EmissionColor", new Color(1f, 0f, 0f, 1f) * intensity);
            
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    public void ResetIsUsed()
    {
        _isUsed = false;
    }

    public abstract void OnClickDecisionButton();
}