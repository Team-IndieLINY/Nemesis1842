using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DatabaseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private TextMeshProUGUI _buttonText;

    [SerializeField]
    private Color _selectedTextColor;

    [SerializeField]
    private Color _unSelectedTextColor;

    private void Awake()
    {
        _buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void ChangeSelectedButtonTextColor()
    {
        _buttonText.DOColor(_selectedTextColor, 0.1f);
    }
    
    public void ChangeUnSelectedButtonTextColor()
    {
        _buttonText.DOColor(_unSelectedTextColor, 0.1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeSelectedButtonTextColor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeUnSelectedButtonTextColor();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeUnSelectedButtonTextColor();
    }
}
