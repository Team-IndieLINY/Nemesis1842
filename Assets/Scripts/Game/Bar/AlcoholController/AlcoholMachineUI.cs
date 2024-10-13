using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AlcoholMachine))]
public class AlcoholMachineUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform _arrowRectTransform;

    [SerializeField]
    private RectTransform _gaugeBarBackgroundRectTransform;

    [SerializeField]
    private TextMeshProUGUI _currentAlcoholText;

    private AlcoholMachine _alcoholMachine;
    private float _alcoholGaugeHeightPerAlcohol;

    private void Awake()
    {
        _alcoholMachine = GetComponent<AlcoholMachine>();
        _alcoholGaugeHeightPerAlcohol = _gaugeBarBackgroundRectTransform.sizeDelta.y * 0.01f;
    }

    public void UpdateAlcoholMachineUI()
    {
        _currentAlcoholText.text = _alcoholMachine.CurrentAlcohol.ToString();
        
        _arrowRectTransform.anchoredPosition = new Vector2(_arrowRectTransform.anchoredPosition.x
            , _alcoholMachine.CurrentAlcohol * _alcoholGaugeHeightPerAlcohol);
    }
}
