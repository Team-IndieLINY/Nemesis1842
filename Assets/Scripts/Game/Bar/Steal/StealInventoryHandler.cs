using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StealInventoryHandler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public static StealInventoryHandler Inst { get; private set; }

    private void Awake()
    {
        Inst = this;
    }

    private bool _isHovered;
    public bool IsHovered => _isHovered;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovered = false;
    }
}
