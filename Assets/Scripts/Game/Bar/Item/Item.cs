using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private ItemData _itemData;
    public ItemData ItemData => _itemData;
    
    [SerializeField]
    private int _itemAmount = 2;
    public int ItemAmount => _itemAmount;

    [SerializeField]
    private ItemTooltipUI _itemTooltipUI;

    [SerializeField]
    private AlcoholController _alcoholController;

    private ItemUI _itemUI;

    private void Awake()
    {
        _itemUI = GetComponent<ItemUI>();
    }

    public void SetItem(int amount)
    {
        _itemAmount = amount;
    }

    public void IncreaseAmount(int amount)
    {
        _itemAmount += amount;
        _itemUI.UpdateItemUI();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _itemTooltipUI.UpdateTooltipUI(_itemData);
        _itemTooltipUI.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _itemTooltipUI.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_alcoholController.CurrentItem == this)
        {
            return;
        }
        
        _itemAmount--;
        _itemUI.UpdateItemUI();
        _alcoholController.EquipItem(this);
    }
}