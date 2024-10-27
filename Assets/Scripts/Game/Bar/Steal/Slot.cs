using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SlotUI))]
public class Slot : MonoBehaviour
{
    private InformationItemData _informationItemData;
    public InformationItemData InformationItemData => _informationItemData;
    private SlotUI _slotUI;
    
    private void Awake()
    {
        _slotUI = GetComponent<SlotUI>();
    }
    
    public bool IsEmpty()
    {
        return _informationItemData == null;
    }

    public void SelectSlot()
    {
        _slotUI.SelectSlotUI();
    }

    public void UnSelectSlot()
    {
        _slotUI.UnSelectSlotUI();
    }

    public void AddItem(InformationItemData informationItemData)
    {
        _informationItemData = informationItemData;
        _slotUI.UpdateSlotUI();
    }
    
    public void ActivateSlot()
    {
        _slotUI.ActivateSlotUI();
    }

    public void InActivateSlot()
    {
        _slotUI.InActivateSlotUI();
    }
}