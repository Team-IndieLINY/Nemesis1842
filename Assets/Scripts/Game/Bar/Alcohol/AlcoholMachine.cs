using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AlcoholMachineUI))]
public class AlcoholMachine : MonoBehaviour
{
    [SerializeField]
    private CocktailMakingManager _cocktailMakingManager;
    [SerializeField]
    private Slider _tensSlider;
    
    [SerializeField]
    private Slider _hundredsSlider;
    
    private int _currentAlcohol = 0;
    public int CurrentAlcohol => _currentAlcohol;
    
    private AlcoholMachineUI _alcoholMachineUI;

    private void Awake()
    {
        _alcoholMachineUI = GetComponent<AlcoholMachineUI>();
    }

    public void UpdateAlcoholGauge()
    {
        _currentAlcohol = (int)(_tensSlider.value + _hundredsSlider.value * 10);
        
        _alcoholMachineUI.UpdateAlcoholMachineUI();
    }

    public void SetAlcohol()
    {
        _cocktailMakingManager.SetAlcohol(_currentAlcohol);
    }
}
