using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemEquipmentSlotHandler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [SerializeField]
    private Transform _slotTransform;

    [SerializeField]
    private AlcoholController _alcoholController;
    
    private Vector3 _slotPosition;
    public Vector3 SlotPosition => _slotPosition;
    
    private bool _isHovered;
    public bool IsHovered => _isHovered;

    private void Awake()
    {
        _slotPosition = _slotTransform.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovered = false;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        _alcoholController.ResetItemSlot();
    }
}
