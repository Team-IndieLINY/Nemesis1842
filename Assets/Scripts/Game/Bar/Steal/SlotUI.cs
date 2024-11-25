using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Slot))]
public class SlotUI : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private Inventory _inventory;

    [SerializeField]
    private Image _slotImage;
    [SerializeField]
    private Image _itemImage;

    [SerializeField]
    private Sprite _selectedSlotSprite;

    [SerializeField]
    private Sprite _unSelectedSlotSprite;
    private Slot _slot;
    
    private void Awake()
    {
        _slot = GetComponent<Slot>();
        InActivateSlotUI();
        
        _itemImage.gameObject.SetActive(false);
    }
    
    public void UpdateSlotUI()
    {
        if (_slot.IsEmpty())
        {
            InActivateSlotUI();
            _itemImage.gameObject.SetActive(false);
            _itemImage.sprite = null;
            return;
        }

        ActivateSlotUI();
        _itemImage.sprite = _slot.InformationItemData.ItemSprite;
        _itemImage.SetNativeSize();
        
        var sizeDelta = _itemImage.rectTransform.sizeDelta;
        sizeDelta =
            new Vector2(sizeDelta.x * 0.7f, sizeDelta.y * 0.7f);
        
        _itemImage.rectTransform.sizeDelta = sizeDelta;
        
        _itemImage.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Inst.PlaySFX("inventory_item_click");
        _inventory.SelectSlot(_slot);
    }

    public void SelectSlotUI()
    {
        _slotImage.sprite = _selectedSlotSprite;
    }

    public void UnSelectSlotUI()
    {
        _slotImage.sprite = _unSelectedSlotSprite;
    }

    public void ActivateSlotUI()
    {
        _slotImage.raycastTarget = true;
        _itemImage.raycastTarget = true;
    }

    public void InActivateSlotUI()
    {
        _slotImage.raycastTarget = false;
        _itemImage.raycastTarget = false;
    }
}