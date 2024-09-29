using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlcoholDial : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private int _increasedAmount;

    [SerializeField]
    private AlcoholMachine _alcoholMachine;
    
    private bool _isHovered;
    private void Update()
    {
        if (_isHovered is false)
        {
            return;
        }
        
        float mouseWheelAxis = Input.GetAxisRaw("Mouse ScrollWheel");
        if (mouseWheelAxis > 0)
        {
            _alcoholMachine.SetCurrentAlcohol(_increasedAmount);
        }
        else if (mouseWheelAxis < 0)
        {
            _alcoholMachine.SetCurrentAlcohol(-_increasedAmount);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovered = false;
    }
}
